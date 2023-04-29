using LevelWindowModule.View;
using MainModule;
using MvvmModule;
using SettingsWindowModule.Contracts;

namespace LevelWindowModule
{
    public sealed class LevelWindowViewModel : EmptyViewModel, ILevelWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        private readonly LevelRestarter _playerRestarter;

        public LevelWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            LevelRestarter playerRestarter) : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
            _playerRestarter = playerRestarter;
        }

        public void SettingsClickFromView()
        {
            _settingsWindowPresenter.ShowWindow();
        }

        public void RestartLevelClickFromView()
        {
            _playerRestarter.RestartLevel();
        }
    }
}