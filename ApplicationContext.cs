using System;
using System.Windows.Forms;

namespace Thinkpad_Backlight
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private readonly Form1 _configWindow = new Form1();

        public ApplicationContext()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.TrayIcon,
                ContextMenu = new ContextMenu(menuItems: new[]
                {
                    new MenuItem(text: "On: Bright", onClick: (_, __) => KeyboardController.ToggleBacklight(KeyboardBrightness.Bright)),
                    new MenuItem(text: "On: Dim", onClick: (_, __) => KeyboardController.ToggleBacklight(KeyboardBrightness.Dim)),
                    new MenuItem(text: "Off", onClick: (_, __) => KeyboardController.ToggleBacklight(KeyboardBrightness.Off)),
                    new MenuItem(text: "Settings", onClick: ShowConfig),
                    new MenuItem(text: "Exit", onClick: (_, __) => Application.Exit())
                }),
                Visible = true
            };
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