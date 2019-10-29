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

using System.Windows.Forms;
using Settings = Thinkpad_Backlight.Properties.Settings;

namespace Thinkpad_Backlight
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private NotifyIcon _trayIcon;

        public ApplicationContext()
        {
            var keyboardController = new KeyboardController();

            if (Settings.Default.EnableAtStartup)
                keyboardController.ToggleBacklight(allowInTerminalServerSession: false);

            var brightMenuItem = new MenuItem("On: Bright");
            var dimMenuItem = new MenuItem("On: Dim");
            var timerMenuItem = new MenuItem("Timer") { Checked = Settings.Default.Timer };
            var keypressMenuItem = new MenuItem("Monitor key presses") { Checked = Settings.Default.MonitorKeys };
            var settingsMenuItem = new MenuItem(text: "Settings") /* { BarBreak = true }*/;

            _trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.TrayIcon,
                ContextMenu = new ContextMenu(menuItems: new[]
                {
                    brightMenuItem,
                    dimMenuItem,
                    new MenuItem(text: "Off", onClick: (_, __) => keyboardController.ToggleBacklight(KeyboardBrightness.Off)),
                    timerMenuItem,
                    keypressMenuItem,
                    new MenuItem("-"), // or use BarBreak instead, on the next item, to seperate vertically
                    settingsMenuItem,
                    new MenuItem(text: "Exit", onClick: (_, __) => Application.Exit())
                }),
                Visible = false,
                Text = "Thinkpad Backlight"
            };

            var configWindow = new Form1(brightMenuItem, dimMenuItem, timerMenuItem, keypressMenuItem, keyboardController);

            _trayIcon.DoubleClick += configWindow.ShowConfig;
            settingsMenuItem.Click += configWindow.ShowConfig;
            _trayIcon.Visible = true;
        }

        /// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" /> and optionally releases the managed resources.</summary>
        /// <param name="disposing">
        /// <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_trayIcon != null)
            {
                _trayIcon.Dispose();
                _trayIcon = null;
            }

            base.Dispose(disposing: disposing);
        }
    }
}