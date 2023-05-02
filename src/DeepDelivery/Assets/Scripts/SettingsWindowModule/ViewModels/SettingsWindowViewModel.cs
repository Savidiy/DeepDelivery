using System;
using AudioModule;
using AudioModule.Contracts;
using MainModule;
using MvvmModule;
using SettingsWindowModule.View;

namespace SettingsWindowModule
{
    public sealed class SettingsWindowViewModel : EmptyViewModel, ISettingsWindowViewModel
    {
        private readonly InputSettings _inputSettings;
        private readonly ProgressUpdater _progressUpdater;
        private readonly MainStateMachine _mainStateMachine;
        private readonly AudioSettings _audioSettings;
        private readonly IAudioPlayer _audioPlayer;
        private readonly Type _returnStateType;

        public event Action NeedClose;
        public float SoundVolume { get; }
        public float MusicVolume { get; }

        public SettingsWindowViewModel(IViewModelFactory viewModelFactory, InputSettings inputSettings,
            ProgressUpdater progressUpdater, MainStateMachine mainStateMachine, AudioSettings audioSettings,
            IAudioPlayer audioPlayer) : base(viewModelFactory)
        {
            _inputSettings = inputSettings;
            _progressUpdater = progressUpdater;
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
            _progressUpdater.ResetProgress();
            NeedClose?.Invoke();
            _mainStateMachine.EnterToState<StartMainState>();
        }

        public void UseMobileInputFromView(bool isUseMobileInput)
        {
            _audioPlayer.PlayClick();
            _inputSettings.SetControls(isUseMobileInput ? EControlType.Mobile : EControlType.Keyboard);
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