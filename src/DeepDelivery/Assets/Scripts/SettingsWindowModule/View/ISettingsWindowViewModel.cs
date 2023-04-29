using MvvmModule;
using UniRx;

namespace SettingsWindowModule.View
{
    public interface ISettingsWindowViewModel : IViewModel
    {
        void BackClickFromView();
        void ResetClickFromView();
        void SelectMobileFromView();
        void SelectKeyboardFromView();
        void SetSoundVolumeFromView(float volume);
        void SetMusicVolumeFromView(float volume);
        float SoundVolume { get; }
        float MusicVolume { get; }
    }
}