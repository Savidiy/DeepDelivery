using MvvmModule;

namespace SettingsWindowModule.View
{
    public sealed class SettingsWindowView : View<SettingsWindowHierarchy, ISettingsWindowViewModel>
    {
        public SettingsWindowView(SettingsWindowHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(ISettingsWindowViewModel viewModel)
        {
            Hierarchy.SoundVolume.value = viewModel.SoundVolume;
            Hierarchy.MusicVolume.value = viewModel.MusicVolume;
            Hierarchy.UseMobileInput.isOn = viewModel.IsUseMobileInput;
                
            BindClick(Hierarchy.ResetButton, OnResetButtonClick);
            BindClick(Hierarchy.BackButton, OnBackButtonClick);
            BindToggle(Hierarchy.UseMobileInput, OnUseMobileInputChanged);
            BindSlider(Hierarchy.MusicVolume, OnMusicVolumeSet);
            BindSlider(Hierarchy.SoundVolume, OnSoundVolumeSet);
        }

        private void OnSoundVolumeSet(float volume) => ViewModel.SetSoundVolumeFromView(volume);

        private void OnMusicVolumeSet(float volume) => ViewModel.SetMusicVolumeFromView(volume);

        private void OnBackButtonClick() => ViewModel.BackClickFromView();

        private void OnResetButtonClick() => ViewModel.ResetClickFromView();

        private void OnUseMobileInputChanged(bool isUseMobileInput) => ViewModel.UseMobileInputFromView(isUseMobileInput);

    }
}