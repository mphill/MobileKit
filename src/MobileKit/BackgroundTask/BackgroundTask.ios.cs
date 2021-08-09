using System;
using System.Diagnostics;
using MobileKit.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace MobileKit
{
    public partial class BackgroundTask
    {
        private nint _bgTask;


        public void Stop()
        {
            Debug.WriteLine($"Ended task id: {_bgTask}");
            UIApplication.SharedApplication.EndBackgroundTask(_bgTask);
        }

        public void Start()
        {
            _bgTask = UIApplication.SharedApplication.BeginBackgroundTask(() =>
            {
                Debug.WriteLine("Exhausted time");
                UIApplication.SharedApplication.EndBackgroundTask(_bgTask);
            });

            Debug.WriteLine($"Started task id: {_bgTask}");
        }

        public void Status()
        {
            Debug.WriteLine($"{UIApplication.SharedApplication.BackgroundTimeRemaining}");
        }
    }
}
