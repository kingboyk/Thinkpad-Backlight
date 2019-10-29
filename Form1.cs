using System;
using System.Configuration;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Settings = Thinkpad_Backlight.Properties.Settings;

namespace Thinkpad_Backlight
{
    internal partial class Form1 : Form
    {
        private IKeyboardMouseEvents _globalHook;
        private readonly MenuItem _timerMenuItem;
        private readonly MenuItem _keypressMenuItem;
        private readonly KeyboardController _keyboardController;

        public Form1(MenuItem brightMenuItem, MenuItem dimMenuItem, MenuItem timerMenuItem, MenuItem keypressMenuItem, KeyboardController keyboardController)
        {
            if (brightMenuItem == null) throw new ArgumentNullException(nameof(brightMenuItem));
            if (dimMenuItem == null) throw new ArgumentNullException(nameof(dimMenuItem));
            _timerMenuItem = timerMenuItem ?? throw new ArgumentNullException(nameof(timerMenuItem));
            _keypressMenuItem = keypressMenuItem ?? throw new ArgumentNullException(nameof(keypressMenuItem));
            _keyboardController = keyboardController ?? throw new ArgumentNullException(nameof(keyboardController));

            InitializeComponent();
            Icon = Properties.Resources.TrayIcon;

            brightMenuItem.Click += (sender, args) =>
            {
                timer1.Reset();
                _keyboardController.ToggleBacklight(KeyboardBrightness.Bright);
            };

            dimMenuItem.Click += (sender, args) =>
            {
                timer1.Reset();
                _keyboardController.ToggleBacklight(KeyboardBrightness.Dim);
            };

            timerMenuItem.Click += TimerMenuItemOnClick;

            keypressMenuItem.Click += KeypressMenuItemOnClick;

            if (Settings.Default.Seconds < 1)
                throw new ConfigurationErrorsException("The seconds setting must be 1 or more");

            if (Settings.Default.Timer)
            {
                timer1.Interval = Settings.Default.Seconds * 1000;
                timer1.Start();
            }

            if (Settings.Default.MonitorKeys)
                SubscribeToKeyDownEvents();

            BrightnessComboBox.SelectedIndex = Settings.Default.Bright ? 1 : 0;
            BrightnessComboBox.SelectedIndexChanged += (sender, args) => 
            {
                Settings.Default.Bright = BrightnessComboBox.SelectedIndex == 1;
                Settings.Default.Save();
            };

            OnStartupCheckBox.Checked = Settings.Default.EnableAtStartup;
            OnStartupCheckBox.CheckedChanged += (sender, args) =>
            {
                Settings.Default.EnableAtStartup = OnStartupCheckBox.Checked;
                Settings.Default.Save();
            };

            OnKeyPressCheckBox.Checked = Settings.Default.MonitorKeys;
            OnKeyPressCheckBox.CheckedChanged += KeypressMenuItemOnClick;

            TimerCheckBox.Checked = Settings.Default.Timer;
            TimerCheckBox.CheckedChanged += TimerMenuItemOnClick;

            SecondsNumeric.Maximum = ushort.MaxValue;
            SecondsNumeric.Value = Settings.Default.Seconds;
            SecondsDisplayLabel.Text = $"({SecondsNumeric.Value})";
            SecondsNumeric.ValueChanged += (sender, args) => 
            {
                ushort seconds = (ushort)SecondsNumeric.Value;
                SecondsDisplayLabel.Text = $"({seconds})";
                timer1.Interval = seconds * 1000;
                Settings.Default.Seconds = seconds;
                Settings.Default.Save();
            };

            void TimerMenuItemOnClick(object sender, EventArgs args)
            {
                if (timerMenuItem.Checked)
                {
                    timerMenuItem.Checked = false;
                    Settings.Default.Timer = false;
                    timer1.Stop();
                }
                else
                {
                    timerMenuItem.Checked = true;
                    Settings.Default.Timer = true;
                    timer1.Start();
                }

                Settings.Default.Save();
            }

            void KeypressMenuItemOnClick(object sender, EventArgs args)
            {
                if (keypressMenuItem.Checked)
                {
                    keypressMenuItem.Checked = false;
                    Settings.Default.MonitorKeys = false;
                    UnsubscribeFromKeyDownEvents();
                }
                else
                {
                    keypressMenuItem.Checked = true;
                    Settings.Default.MonitorKeys = true;
                    SubscribeToKeyDownEvents();
                }

                Settings.Default.Save();
            }
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
            if (e.KeyCode != Keys.None /* issue 1 */)
                _keyboardController.ToggleBacklight(allowInTerminalServerSession: false);

            if (Settings.Default.Timer)
            {
                timer1.Reset();
            }
        }

        private void UnsubscribeFromKeyDownEvents()
        {
            if (_globalHook != null)
            {
                _globalHook.KeyDown -= GlobalHookOnKeyDown;
                _globalHook.Dispose();
            }
        }

        private void Timer1Tick(object sender, EventArgs e) => _keyboardController.ToggleBacklight(KeyboardBrightness.Off);

        private void Form1FormClosed(object sender, FormClosedEventArgs e)
        {
            _keypressMenuItem.Enabled = true;
            _timerMenuItem.Enabled = true;
        }

        internal void ShowConfig(object sender, EventArgs e)
        {
            _keypressMenuItem.Enabled = false;
            _timerMenuItem.Enabled = false;

            // If we are already showing the window, merely focus it.
            if (Visible)
            {
                Activate();
            }
            else
            {
                ShowDialog();
            }
        }

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