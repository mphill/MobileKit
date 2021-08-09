using System;
namespace MobileKit.Interfaces
{
    public interface IAudio
    {
        void Play(string filename, bool loop = false, Action onCompleted = null);
        void Stop();
        void Volume(double value);
        void Toggle();
        bool IsPlaying { get; }
    }
}
