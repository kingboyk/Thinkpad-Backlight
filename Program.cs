using System;
using System.Windows.Forms;

namespace Thinkpad_Backlight
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Keyboard_Core.KeyboardControl().SetKeyboardBackLightStatus(0);
            MessageBox.Show(SystemInformation.TerminalServerSession.ToString(), "Is RDC?");
            //Application.Run(new Form1());
        }
    }
}
