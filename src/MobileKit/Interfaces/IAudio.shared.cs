using System;
namespace MobileKit.Interfaces
{
    public interface IAudio
    {
        void Play(string filename, bool loop = false);
        void Stop();
        void Volume(double value);
        void Toggle();
    }
}
