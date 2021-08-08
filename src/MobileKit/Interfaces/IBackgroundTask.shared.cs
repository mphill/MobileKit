using System;
namespace MobileKit.Interfaces
{
    public interface IBackgroundTask
    {
        void Start();
        void Stop();
        void Run(TimeSpan delay, Action command);
        void Status();
    }
}
