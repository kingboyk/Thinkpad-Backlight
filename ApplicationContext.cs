using System;
using System.Windows.Forms;
using Settings = Thinkpad_Backlight.Properties.Settings;

namespace Thinkpad_Backlight
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private readonly Form1 _configWindow;

        public ApplicationContext()
        {
            var keyboardController = new KeyboardController();

            if (Settings.Default.EnableAtStartup)
                keyboardController.ToggleBacklight(allowInTerminalServerSession: false);

            var brightMenuItem = new MenuItem("On: Bright");
            var dimMenuItem = new MenuItem("On: Dim");
            var timerMenuItem = new MenuItem("Timer") { Checked = Settings.Default.Timer };
            var keypressMenuItem = new MenuItem("Monitor key presses") { Checked = Settings.Default.MonitorKeys };

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
                    new MenuItem(text: "Settings", onClick: ShowConfig)/* { BarBreak = true }*/,
                    new MenuItem(text: "Exit", onClick: (_, __) => Application.Exit())
                }),
                Visible = false,
                Text = "Thinkpad Backlight"
            };

            _trayIcon.DoubleClick += ShowConfig;

            _configWindow = new Form1(brightMenuItem, dimMenuItem, timerMenuItem, keypressMenuItem, keyboardController);
            _trayIcon.Visible = true;
        }

        private void ShowConfig(object sender, EventArgs e)
        {
            // If we are already showing the window, merely focus it.
            if (_configWindow.Visible)
            {
                _configWindow.Activate();
            }
            else
            {
                _configWindow.ShowDialog();
            }
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