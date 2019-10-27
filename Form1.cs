using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace Thinkpad_Backlight
{
    public partial class Form1 : Form
    {
        private readonly bool _timerIsEnabled = Properties.Settings.Default.Seconds > 0; // Where 0 is timer off
        private IKeyboardMouseEvents _globalHook;

        public Form1()
        {
            InitializeComponent();

            if (_timerIsEnabled)
            {
                timer1.Interval = Properties.Settings.Default.Seconds * 1000;
                timer1.Start();
                Subscribe();
            }
        }

        private void Subscribe()
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

        private void Unsubscribe()
        {
            if (_globalHook != null)
            {
                _globalHook.KeyDown -= GlobalHookOnKeyDown;
                _globalHook.Dispose();
            }
        }

        private void Timer1Tick(object sender, System.EventArgs e) => KeyboardController.ToggleBacklight(KeyboardBrightness.Off);

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            Unsubscribe();

            // ReSharper disable once UseNullPropagation
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
