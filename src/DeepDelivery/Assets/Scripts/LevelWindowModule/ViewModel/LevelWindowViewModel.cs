using LevelWindowModule.View;
using MainModule;
using MvvmModule;
using Savidiy.Utils;
using SettingsWindowModule.Contracts;
using UniRx;

namespace LevelWindowModule
{
    public sealed class LevelWindowViewModel : EmptyViewModel, ILevelWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        private readonly LevelRestarter _playerRestarter;
        private readonly PlayerHolder _playerHolder;
        private readonly ReactiveProperty<int> _heartCount = new();

        public IReadOnlyReactiveProperty<int> HeartCount => _heartCount;

        public LevelWindowViewModel(IViewModelFactory viewModelFactory, ISettingsWindowPresenter settingsWindowPresenter,
            LevelRestarter playerRestarter, TickInvoker tickInvoker, PlayerHolder playerHolder) : base(viewModelFactory)
        {
            _settingsWindowPresenter = settingsWindowPresenter;
            _playerRestarter = playerRestarter;
            _playerHolder = playerHolder;

            tickInvoker.Updated += OnUpdated;
            OnUpdated();
        }

        private void OnUpdated()
        {
            _heartCount.Value = _playerHolder.Player.CurrentHp;
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