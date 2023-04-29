using MainModule;
using MvvmModule;
using SettingsWindowModule.Contracts;
using StartWindowModule.View;

namespace StartWindowModule
{
    public sealed class StartWindowViewModel : EmptyViewModel, IStartWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        private readonly MainStateMachine _mainStateMachine;

        public bool HasProgress { get; }

        public StartWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            MainStateMachine mainStateMachine) : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
            _mainStateMachine = mainStateMachine;
        }

        public void StartClickFromView()
        {
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }

        public void ContinueClickFromView()
        {
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }

        public void SettingsClickFromView()
        {
            _settingsWindowPresenter.ShowWindow();
        }
    }
}