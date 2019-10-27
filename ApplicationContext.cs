using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Thinkpad_Backlight
{
    public class ApplicationContext : System.Windows.Forms.ApplicationContext
    {
        private NotifyIcon _trayIcon;
        private readonly Form1 _configWindow = new Form1();
        private IKeyboardMouseEvents _globalHook;

        public ApplicationContext()
        {
            Subscribe(); // HACK
            // Initialize Tray Icon
            _trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.TrayIcon,
                ContextMenu = new ContextMenu(menuItems: new[]
                {
                    new MenuItem(text: "Settings", onClick: ShowConfig),
                    new MenuItem(text: "Exit", onClick: (sender, e) => Application.Exit())
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

        private void Subscribe()
        {
            if (_globalHook == null)
            {
                // Note: for the application hook, use the Hook.AppEvents() instead
                _globalHook = Hook.GlobalEvents();
                _globalHook.KeyPress += GlobalHookKeyPress;
                _globalHook.KeyDown += GlobalHookOnKeyDown;

                Hook.GlobalEvents().OnCombination(new Dictionary<Combination, Action>
                {
                    { Combination.TriggeredBy(Keys.Control), () => { Console.WriteLine("You Pressed CTL"); }},
                    { Combination.TriggeredBy(Keys.E), () => { Console.WriteLine("You Pressed e"); }}
                });
            }
        }

        private static void GlobalHookOnKeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("KeyDown: \t{0}", e.KeyCode);
        }

        private static void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

        private void Unsubscribe()
        {
            if (_globalHook != null)
            {
                _globalHook.KeyPress -= GlobalHookKeyPress;
                _globalHook.KeyDown -= GlobalHookOnKeyDown;
                _globalHook.Dispose();
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

            Unsubscribe();

            base.Dispose(disposing: disposing);
        }
    }
}