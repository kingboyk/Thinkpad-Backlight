/*
Copyright © Stephen Kennedy 2019  

This file is part of Thinkpad-Backlight.

Thinkpad-Backlight is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Thinkpad-Backlight is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Thinkpad-Backlight.  If not, see <https://www.gnu.org/licenses/>.
*/

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
            AppDomain.CurrentDomain.UnhandledException += (sender, args) => HandleUnhandledException(args.ExceptionObject as Exception);
            Application.ThreadException += (sender, args) => HandleUnhandledException(args.Exception);

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

        private static void HandleUnhandledException(Exception ex)
        {
            MessageBox.Show($@"There was an error and the program will now exit.

Error message: {ex.Message}

Stack trace: {ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
        }
    }
}
