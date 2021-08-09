using System;
using Android.Media;
using MobileKit.Interfaces;
using Xamarin.Forms;

namespace MobileKit
{
    public partial class Audio : IAudio
    {
        //https://developer.android.com/guide/topics/media/mediaplayer
        private MediaPlayer _player;
        private Action _onCompleted;

        

        public void Play(string filename, bool loop = false, Action onCompleted = null)
        {
            _onCompleted = onCompleted;

            var afd = Android.App.Application.Context.Assets.OpenFd(filename);

            //var audioStream = Android.App.Application.Context.Assets.Open(filename);
           
            _player = new MediaPlayer();
            
            _player.Looping = loop;
            _player.Completion += _mediaPlayer_Completion;
            _player.SetDataSource(afd);
            _player.Prepare();
            _player.Start();
        }

        private void _mediaPlayer_Completion(object sender, EventArgs e)
        {
            _onCompleted?.Invoke();
        }

        public bool IsPlaying => _player?.IsPlaying ?? false;

        public void Stop()
        {
            _player.Stop();
        }

        public void Toggle()
        {
            if (_player.IsPlaying)
            {
                _player.Pause();
            }
            else
            {
                _player.Start();
            }
        }

        public void Volume(double value)
        {
            _player.SetVolume((float)value, (float)value);
        }
    }
}
