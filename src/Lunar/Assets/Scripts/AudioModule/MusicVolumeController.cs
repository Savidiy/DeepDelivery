using LevelWindowModule;
using LevelWindowModule.Contracts;
using MvvmModule;
using UniRx;
using UnityEngine;

namespace AudioModule
{
    public sealed class MusicVolumeController : DisposableCollector
    {
        private readonly AudioSource _musicSource;

        public MusicVolumeController(ICameraProvider cameraProvider, AudioSettings audioSettings, AudioLibrary audioLibrary)
        {
            _musicSource = cameraProvider.Camera.gameObject.AddComponent<AudioSource>();
            _musicSource.clip = audioLibrary.MusicClip;
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;
            AddDisposable(audioSettings.MusicVolume.Subscribe(OnMusicVolumeChange));
        }

        private void OnMusicVolumeChange(float volume)
        {
            _musicSource.volume = volume;
            
            if (volume == 0)
                _musicSource.Stop();
            else if (!_musicSource.isPlaying)
                _musicSource.Play();
        }
    }
}