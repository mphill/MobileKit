using System;
namespace MobileKit
{
    public class Audio : IAudio
    {
        private MediaPlayer _mediaPlayer;

        private Audio()
        {
        }

        public IAudio Init(string filename)
        {
            return new Audio();
        }

        public void Play()
        {
            _mediaPlayer = new MediaPlayer();
            //_mediaPlayer.SetVolume(BackgroundMusicVolume, BackgroundMusicVolume);
            _mediaPlayer.Looping = true;
            _mediaPlayer.Completion += _mediaPlayer_Completion;
            //_mediaPlayer.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
            _mediaPlayer.Prepare();
            _mediaPlayer.Start();
        }

        private void _mediaPlayer_Completion(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
        }

        public void Toggle()
        {
            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
            }
            else
            {
                _mediaPlayer.Start();
            }
        }
    }
}
