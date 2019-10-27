using System.Windows.Forms;

namespace Thinkpad_Backlight
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private NotifyIcon _trayIcon;

        public ApplicationContext()
        {
            // Initialize Tray Icon
            _trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.TrayIcon,
                ContextMenu = new ContextMenu(menuItems: new[]
                {
                    new MenuItem(text: "Exit", onClick: (sender, e) => Application.Exit())
                }),
                Visible = true
            };
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