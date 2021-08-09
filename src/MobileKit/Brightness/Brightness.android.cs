using System;
using Android.Views;
using Xamarin.Essentials;

namespace MobileKit.Brightness
{
    public static partial class Brightness
    {
        public static void Set(float brightness)
        {
            if (brightness < 0f || brightness > 1f)
            {
                throw new ArgumentOutOfRangeException();
            }

            var window = Platform.CurrentActivity.Window;

            var attributesWindow = new WindowManagerLayoutParams();

            attributesWindow.CopyFrom(window.Attributes);
            attributesWindow.ScreenBrightness = brightness;

            window.Attributes = attributesWindow;
        }
    }
}
