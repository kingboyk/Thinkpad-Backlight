using System;
using System.Configuration;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Thinkpad_Backlight
{
    internal partial class Form1 : Form
    {
        private IKeyboardMouseEvents _globalHook;

        public Form1(MenuItem timerMenuItem, MenuItem keypressMenuItem)
        {
            if (timerMenuItem == null) throw new ArgumentNullException(nameof(timerMenuItem));
            if (keypressMenuItem == null) throw new ArgumentNullException(nameof(keypressMenuItem));

            InitializeComponent();
            Icon = Properties.Resources.TrayIcon;

            timerMenuItem.Click += (sender, args) =>
            {
                if (timerMenuItem.Checked)
                {
                    timerMenuItem.Checked = false;
                    Properties.Settings.Default.Timer = false;
                    timer1.Stop();
                }
                else
                {
                    timerMenuItem.Checked = true;
                    Properties.Settings.Default.Timer = true;
                    timer1.Start();
                }

                Properties.Settings.Default.Save();
            };

            keypressMenuItem.Click += (sender, args) =>
            {
                if (keypressMenuItem.Checked)
                {
                    keypressMenuItem.Checked = false;
                    Properties.Settings.Default.MonitorKeys = false;
                    UnsubscribeFromKeyDownEvents();
                }
                else
                {
                    keypressMenuItem.Checked = true;
                    Properties.Settings.Default.MonitorKeys = true;
                    SubscribeToKeyDownEvents();
                }

                Properties.Settings.Default.Save();
            };

            if (Properties.Settings.Default.Seconds < 1)
                throw new ConfigurationErrorsException("The seconds setting must be 1 or more");

            if (Properties.Settings.Default.Timer)
            {
                timer1.Interval = Properties.Settings.Default.Seconds * 1000;
                timer1.Start();
            }

            if (Properties.Settings.Default.MonitorKeys)
                SubscribeToKeyDownEvents();
        }

        private void SubscribeToKeyDownEvents()
        {
            if (_globalHook == null)
            {
                // Note: for the application hook, use the Hook.AppEvents() instead
                _globalHook = Hook.GlobalEvents();
                _globalHook.KeyDown += GlobalHookOnKeyDown;
            }
        }

        private void GlobalHookOnKeyDown(object sender, KeyEventArgs e)
        {
            if (!SystemInformation.TerminalServerSession)
                KeyboardController.ToggleBacklight(Properties.Settings.Default.Bright ? 2 : 1);

            timer1.Stop();
            timer1.Start();
        }

        private void UnsubscribeFromKeyDownEvents()
        {
            if (_globalHook != null)
            {
                _globalHook.KeyDown -= GlobalHookOnKeyDown;
                _globalHook.Dispose();
            }
        }

        private void Timer1Tick(object sender, EventArgs e) => KeyboardController.ToggleBacklight(KeyboardBrightness.Off);

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            UnsubscribeFromKeyDownEvents();

            // ReSharper disable once UseNullPropagation
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
