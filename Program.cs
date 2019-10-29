using System;
using System.IO;
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
            Application.SetCompatibleTextRenderingDefault(defaultValue: false);

            ApplicationContext context;
            try
            {
                context = new ApplicationContext();
            }
            catch (FileNotFoundException)
            {
                Application.Exit();
                return;
            }

            Application.Run(context);
        }
    }
}
