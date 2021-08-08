using System;
using System.Diagnostics;
using MobileKit.Interfaces;
using UIKit;
using Xamarin.Forms;

namespace MobileKit
{
    public partial class BackgroundTask : IBackgroundTask
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

        public void Run(TimeSpan delay, Action command)
        {

            var end = DateTime.Now.AddSeconds(delay.TotalSeconds);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Debug.WriteLine($"{DateTime.Now}");

                var result = DateTime.Now < end;

                //BackgroundTask.Instance.Status();

                if (!result)
                {
                    command?.Invoke();
                    BackgroundTask.Instance.Stop();
                }

                return result;
            });
        }
    }
}
