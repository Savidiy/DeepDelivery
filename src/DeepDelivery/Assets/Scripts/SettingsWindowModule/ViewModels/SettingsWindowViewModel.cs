using System;
using AudioModule;
using AudioModule.Contracts;
using MainModule;
using MvvmModule;
using Progress;
using SettingsWindowModule.View;

namespace SettingsWindowModule
{
    public sealed class SettingsWindowViewModel : EmptyViewModel, ISettingsWindowViewModel
    {
        private readonly ProgressProvider _progressProvider;
        private readonly MainStateMachine _mainStateMachine;
        private readonly AudioSettings _audioSettings;
        private readonly IAudioPlayer _audioPlayer;
        private readonly Type _returnStateType;

        public event Action NeedClose;
        public float SoundVolume { get; }
        public float MusicVolume { get; }
        
        public SettingsWindowViewModel(IViewModelFactory viewModelFactory, ProgressProvider progressProvider,
            MainStateMachine mainStateMachine, AudioSettings audioSettings, IAudioPlayer audioPlayer)
            : base(viewModelFactory)
        {
            _progressProvider = progressProvider;
            _mainStateMachine = mainStateMachine;
            _audioSettings = audioSettings;
            _audioPlayer = audioPlayer;

            SoundVolume = audioSettings.SoundVolume.Value;
            MusicVolume = audioSettings.MusicVolume.Value;
        }

        public void BackClickFromView()
        {
            _audioPlayer.PlayClick();
            NeedClose?.Invoke();
        }

        public void ResetClickFromView()
        {
            _audioPlayer.PlayClick();
            _progressProvider.ResetProgress();
            NeedClose?.Invoke();
            _mainStateMachine.EnterToState<StartMainState>();
        }

        public void SelectMobileFromView()
        {
            _audioPlayer.PlayClick();
            _progressProvider.SetControls(EControlType.Mobile);
        }

        public void SelectKeyboardFromView()
        {
            _audioPlayer.PlayClick();
            _progressProvider.SetControls(EControlType.Keyboard);
        }

        public void SetSoundVolumeFromView(float volume)
        {
            _audioSettings.SetSoundVolume(volume);
        }

        public void SetMusicVolumeFromView(float volume)
        {
            _audioSettings.SetMusicVolume(volume);
        }
    }
}