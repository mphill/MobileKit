using System;
using Android.Media;
using Android.Media.Session;
using MobileKit.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileKit
{
    public partial class Audio : IAudio
    {
        //https://developer.android.com/guide/topics/media/mediaplayer
        private MediaPlayer _player;
        private Action _onCompleted;
        private bool _nowPlaying;
        private MediaSession _session;

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

        public bool NowPlaying {
            get
            {
                return _nowPlaying;
            }

            set {
                if(value)
                {
                    _session = new MediaSession(Platform.AppContext, "MusicService");

                    var mediaCallback = new MediaSessionCallback();
                    _session.SetCallback(mediaCallback);
                    _session.SetFlags(MediaSessionFlags.HandlesMediaButtons | MediaSessionFlags.HandlesTransportControls);



                    _nowPlaying = true;
                } else
                {
                    _nowPlaying = false;
                }
            }
        }

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

        class MediaSessionCallback : MediaSession.Callback
        {

        }
    }
}
