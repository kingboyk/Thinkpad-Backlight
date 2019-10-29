using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Settings = Thinkpad_Backlight.Properties.Settings;

namespace Thinkpad_Backlight
{
    internal class KeyboardController
    {
        // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable // Avoid implicitly captured closures
        private readonly MethodInfo _setKeyboardBackLightStatusInfo;
        private readonly MethodInfo _getKeyboardBackLightStatusInfo;
        // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

        private readonly Func<int, uint> _setKeyboardBackLightStatusFunc;
        private delegate uint GetKeyboardBackLightStatusDelegate(out int level);
        private readonly GetKeyboardBackLightStatusDelegate _getKeyboardBackLightStatusFunc;

        public KeyboardController()
        {
            var ass = LoadAssembly("Keyboard_Core.dll");
            LoadAssembly("Contract_Keyboard.dll"); // Dependency of Keyboard_Core.dll

            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;

            var keyboardControlType = ass.GetType("Keyboard_Core.KeyboardControl");
            var keyboardControlInstance = Activator.CreateInstance(keyboardControlType);
            _setKeyboardBackLightStatusInfo = keyboardControlType.GetRuntimeMethodsExt("SetKeyboardBackLightStatus");
            _getKeyboardBackLightStatusInfo = keyboardControlType.GetRuntimeMethodsExt("GetKeyboardBackLightStatus");

            _setKeyboardBackLightStatusFunc = level =>
            { 
                var arguments = new object[] { level };
                return (uint)_setKeyboardBackLightStatusInfo.Invoke(keyboardControlInstance, arguments);
            };

            _getKeyboardBackLightStatusFunc = (out int level) =>
            {
                level = -1;
                var arguments = new object[] { level };
                uint r = (uint)_getKeyboardBackLightStatusInfo.Invoke(keyboardControlInstance, arguments);
                level = (int)arguments[0];
                return r;
            };
        }

        private static Assembly LoadAssembly(string dllName)
        {
            try
            {
                return Assembly.LoadFile($"{Settings.Default.DllPath}\\{dllName}");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"Lenovo {dllName} not found. Please find the file and edit the " + nameof(Settings.Default.DllPath) + " setting in this program's app.config file to point to the correct folder location.");
                throw;
            }
        }

        public void ToggleBacklight(KeyboardBrightness brightness)
        {
            int level = brightness switch
            {
                KeyboardBrightness.Off => 0,
                KeyboardBrightness.Dim => 1,
                KeyboardBrightness.Bright =>  2,
                _ =>  throw new ArgumentOutOfRangeException(nameof(brightness))
            };

            ToggleBacklight(level);
        }

        internal void ToggleBacklight(bool allowInTerminalServerSession)
        {
            if (allowInTerminalServerSession || !SystemInformation.TerminalServerSession /* Don't turn backlight on automatically if connected to the machine over RDC */)
                ToggleBacklight(Settings.Default.Bright ? 2 : 1);
        }

        private void ToggleBacklight(int level)
        {
            _getKeyboardBackLightStatusFunc(out int currentLevel);

            if (level != currentLevel)
                _setKeyboardBackLightStatusFunc(level);
        }

        private static Assembly AssemblyResolve(object sender, ResolveEventArgs args) // https://stackoverflow.com/a/15350751/397817
        {
            var ass = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return ass;
        }
    }
}