using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MobileKit.Interfaces;
using Xamarin.Forms;

namespace MobileKit
{
    public partial class BackgroundTask : IBackgroundTask
    {
        public static BackgroundTask Instance => new BackgroundTask();

        public void Run(TimeSpan delay, Action command, CancellationToken token = default)
        {
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(delay, token);
                    command?.Invoke();

                } catch(TaskCanceledException)
                {

                }
                finally
                {
                    BackgroundTask.Instance.Stop();
                }
            });
         }
    }
}
