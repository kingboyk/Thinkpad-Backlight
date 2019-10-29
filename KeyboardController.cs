using System;
using System.Windows.Forms;
using Keyboard_Core;
using Settings = Thinkpad_Backlight.Properties.Settings;

namespace Thinkpad_Backlight
{
    internal static class KeyboardController
    {
        private static readonly KeyboardControl KeyboardControlInstance = new KeyboardControl();

        public static void ToggleBacklight(KeyboardBrightness brightness)
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

        internal static void ToggleBacklight(bool allowInTerminalServerSession)
        {
            if (allowInTerminalServerSession || !SystemInformation.TerminalServerSession /* Don't turn backlight on automatically if connected to the machine over RDC */)
                ToggleBacklight(Settings.Default.Bright ? 2 : 1);
        }

        private static void ToggleBacklight(int level)
        {
            KeyboardControlInstance.GetKeyboardBackLightStatus(out int currentLevel);

            if (level != currentLevel)
                KeyboardControlInstance.SetKeyboardBackLightStatus(level);
        }
    }
}