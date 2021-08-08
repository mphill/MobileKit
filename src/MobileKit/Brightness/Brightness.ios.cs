using System;
using MobileKit.Interfaces;
using UIKit;

namespace MobileKit.Brightness
{
    public static partial class Brightness
    {
        private static nfloat _defaultValue = UIScreen.MainScreen.Brightness;

        public static void Set(float brightness)
        {
            if(brightness < 0f || brightness > 1f)
            {
                throw new ArgumentOutOfRangeException();
            }

            UIScreen.MainScreen.Brightness = brightness;
        }
    }
}
