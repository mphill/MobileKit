using System;
using System.Threading;

namespace MobileKit.Interfaces
{
    public interface IBackgroundTask
    {
        void Start();
        void Stop();
        void Run(TimeSpan delay, Action command, CancellationToken token = default);
        void Status();
    }
}
