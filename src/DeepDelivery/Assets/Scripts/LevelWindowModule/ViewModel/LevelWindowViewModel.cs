using LevelWindowModule.View;
using MvvmModule;
using SettingsWindowModule.Contracts;

namespace LevelWindowModule
{
    public sealed class LevelWindowViewModel : EmptyViewModel, ILevelWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;

        public LevelWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter) : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
        }

        public void SettingsClickFromView()
        {
            _settingsWindowPresenter.ShowWindow();
        }
    }
}