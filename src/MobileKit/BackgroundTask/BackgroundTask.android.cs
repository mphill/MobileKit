using System;
using Android.Content;
using Android.OS;
using Java.Lang;
using Xamarin.Essentials;


namespace MobileKit
{
    public partial class BackgroundTask
    {
        private PowerManager _powerManager;
        private PowerManager.WakeLock _wakeLock;
        private Handler _handler;
        private Runnable _runnable;

        public BackgroundTask()
        {
            _powerManager ??= Platform.AppContext.GetSystemService(Context.PowerService) as PowerManager;
            _wakeLock ??= _powerManager.NewWakeLock(WakeLockFlags.Partial, "rohit_bg_wakelock");
        }



        public void Start()
        {
            if (!_wakeLock.IsHeld)
            {
                _wakeLock.Acquire();
            }

            _handler = new Handler();

            _runnable = new Runnable(() =>
            {
                System.Diagnostics.Debug.WriteLine("emit");
            })
            {

            };

            _handler.Post(_runnable);
        }

        public void Status()
        {
            ///throw new NotImplementedException();
        }

        public void Stop()
        {
            if (_wakeLock.IsHeld)
            {
                _wakeLock.Release();
            }

            if (_handler != null)
            {
                _handler.RemoveCallbacks(_runnable);
            }
        }
    }
}
