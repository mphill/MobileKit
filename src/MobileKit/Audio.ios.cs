using System;
using System.Linq;
using AVFoundation;
using Foundation;
using MobileKit.Interfaces;
using UniformTypeIdentifiers;

namespace MobileKit
{
    public partial class Audio : IAudio
    {

        private AVAudioPlayer _player;
        private AVAudioSession _session;
        private string _file;

        public Audio()
        {
            _session = AVAudioSession.SharedInstance();
            _session.SetCategory(AVAudioSessionCategory.Ambient);
            _session.SetActive(true);
        }

        public void Play(string filename, bool loop = false)
        {
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

        public void Stop()
        {
            _player.Stop();
        }

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
            _ = e;
        }
    }
}
