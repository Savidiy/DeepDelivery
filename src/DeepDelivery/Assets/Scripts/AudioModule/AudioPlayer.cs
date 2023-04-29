using AudioModule.Contracts;
using LevelWindowModule;
using LevelWindowModule.Contracts;
using MvvmModule;
using UniRx;
using UnityEngine;

namespace AudioModule
{
    public sealed class AudioPlayer : DisposableCollector, IAudioPlayer
    {
        private readonly AudioSource _soundSource;
        private readonly AudioLibrary _audioLibrary;
        private readonly AudioSettings _audioSettings;

        public AudioPlayer(ICameraProvider cameraProvider, AudioSettings audioSettings, AudioLibrary audioLibrary)
        {
            _audioSettings = audioSettings;
            _audioLibrary = audioLibrary;

            _soundSource = cameraProvider.Camera.gameObject.AddComponent<AudioSource>();
            _soundSource.loop = false;
            _soundSource.playOnAwake = false;

            AddDisposable(audioSettings.SoundVolume.Subscribe(OnSoundVolumeChange));
        }

        public void PlayClick()
        {
            PlayOnce(SoundId.Click);
        }

        public void PlayOnce(SoundId soundId)
        {
            if (_audioSettings.SoundVolume.Value == 0)
                return;

            AudioClip audioClip = _audioLibrary.GetClip(soundId);
            _soundSource.PlayOneShot(audioClip);
        }

        private void OnSoundVolumeChange(float volume)
        {
            _soundSource.volume = volume;
        }
    }
}