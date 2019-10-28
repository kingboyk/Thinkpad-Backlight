using System;

namespace Thinkpad_Backlight
{
    public static class Extensions
    {
        public static void Reset(this System.Windows.Forms.Timer timer)
        {
            if (timer == null)
                throw new ArgumentNullException(nameof(timer));

            timer.Stop();
            timer.Start();
        }
    }
}