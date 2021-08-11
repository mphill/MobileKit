using System;
using System.Linq;
using AVFoundation;
using Foundation;
using MediaPlayer;
using MobileKit.Interfaces;
using UIKit;
using UniformTypeIdentifiers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileKit
{
    public partial class Audio : IAudio
    {

        private AVAudioPlayer _player;
        private AVAudioSession _session;
        private Action _onCompleted;
        private string _file;
        private MPRemoteCommandCenter _commandCenter = MPRemoteCommandCenter.Shared;
        private bool _nowPlaying;

        public Audio()
        {
            _session = AVAudioSession.SharedInstance();
            _session.SetCategory(AVAudioSessionCategory.Playback); // AVAudioSession.CategoryPlayback ?
            _session.SetActive(true, out var activationError);

            if (activationError != null)
                Console.WriteLine("Could not activate audio session {0}", activationError.LocalizedDescription);


            //AVAudioSession.Notifications.ObserveInterruption(ToneInterruptionListener);

        }

        /// <inheritdoc/>
        public bool IsPlaying => _player?.Playing ?? false;

        

        public MPRemoteCommandHandlerStatus PauseCommand(MPRemoteCommandEvent arg)
        {
            _player.Pause();
            return MPRemoteCommandHandlerStatus.Success;
        }

        /// <inheritdoc/>
        public void Play(string filename, bool loop = false, Action onCompleted = null)
        {
            _onCompleted = onCompleted;
            _file = filename;

            var songURL = NSUrl.FromFilename(filename);

            songURL.CheckPromisedItemIsReachable(out NSError fileError);

            var extension = filename.Split('.').Last();

            var type = UTType.CreateFromExtension(extension).ToString();

            _player = new AVAudioPlayer(songURL, "mp3", out NSError error);

            _player.FinishedPlaying += _player_FinishedPlaying;

            if (loop)
            {
                _player.NumberOfLoops = -1;
            }

            _player.Play();

        }

        /// <inheritdoc/>
        public bool NowPlaying {
            get {
                return _nowPlaying;
            }

            set
            {
                if(value)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        UIApplication.SharedApplication.BeginReceivingRemoteControlEvents();
                    });

                    MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = new MPNowPlayingInfo
                    {
                        Title = AppInfo.Name
                    };

                    _commandCenter.TogglePlayPauseCommand.Enabled = true;
                    _commandCenter.PauseCommand.Enabled = true;
                    _commandCenter.PauseCommand.AddTarget(PauseCommand);
                } else
                {
                    MPNowPlayingInfoCenter.DefaultCenter.NowPlaying = null;
                    _commandCenter.TogglePlayPauseCommand.Enabled = false;
                    _commandCenter.PauseCommand.Enabled = false;
                    //_commandCenter.PauseCommand.RemoveTarget(PauseCommand);

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        UIApplication.SharedApplication.EndReceivingRemoteControlEvents();
                    });
                }
            }
        }

        /// <inheritdoc/>
        public void Stop()
        { 
            _player.Stop();
        }

        /// <inheritdoc/>
        public void Toggle()
        {
            if (_player.Playing)
            {
                _player.Stop();
            }
            else
            {
                _player.Play();
            }
        }

        /// <inheritdoc/>
        public void Volume(double value)
        {
            if (value < 0 || value > 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            _player.SetVolume((float)value, 200);
        }

        private void _player_FinishedPlaying(object sender, AVStatusEventArgs e)
        {
            _onCompleted?.Invoke();
        }
    }
}
