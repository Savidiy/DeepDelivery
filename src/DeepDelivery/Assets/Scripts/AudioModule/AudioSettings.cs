using SettingsModule;
using UniRx;
using UnityEngine;

namespace AudioModule
{
    public class AudioSettings
    {
        private readonly ReactiveProperty<float> _musicVolume = new();
        private readonly ReactiveProperty<float> _soundVolume = new();

        private const string SOUND_VOLUME_KEY = nameof(SOUND_VOLUME_KEY);
        private const string MUSIC_VOLUME_KEY = nameof(MUSIC_VOLUME_KEY);

        public IReadOnlyReactiveProperty<float> MusicVolume => _musicVolume;
        public IReadOnlyReactiveProperty<float> SoundVolume => _soundVolume;

        public AudioSettings(GameSettings gameSettings)
        {
            _musicVolume.Value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, gameSettings.DefaultMusicVolume);
            _soundVolume.Value = PlayerPrefs.GetFloat(SOUND_VOLUME_KEY, gameSettings.DefaultSoundVolume);
        }

        public void SetMusicVolume(float volume)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
            PlayerPrefs.Save();
            _musicVolume.Value = volume;
        }

        public void SetSoundVolume(float volume)
        {
            PlayerPrefs.SetFloat(SOUND_VOLUME_KEY, volume);
            PlayerPrefs.Save();
            _soundVolume.Value = volume;
        }
    }
}