using System;
using Keyboard_Core;

namespace Thinkpad_Backlight
{
    internal static class KeyboardController
    {
        private static readonly KeyboardControl KeyboardControlInstance = new KeyboardControl();

        public static void ToggleBacklight(KeyboardBrightness brightness)
        {
            int level = brightness switch
            {
                KeyboardBrightness.Off => 0,
                KeyboardBrightness.Dim => 1,
                KeyboardBrightness.Bright =>  2,
                _ =>  throw new ArgumentOutOfRangeException(nameof(brightness))
            };

            ToggleBacklight(level);
        }

        internal static void ToggleBacklight(int level)
        {
            KeyboardControlInstance.GetKeyboardBackLightStatus(out int currentLevel);

            if (level != currentLevel)
                KeyboardControlInstance.SetKeyboardBackLightStatus(level);
        }
    }
}