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
        private readonly ReactiveProperty<IItemsViewModel> _items = new();
        private int _itemsCount = int.MinValue;
        
        public IReadOnlyReactiveProperty<int> HeartCount => _heartCount;
        public IReadOnlyReactiveProperty<IItemsViewModel> Items => _items;

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
            Player player = _playerHolder.Player;
            _heartCount.Value = player.CurrentHp;

            UpdateItemsViewModel(player);
        }

        private void UpdateItemsViewModel(Player player)
        {
            int itemsCount = 0;
            foreach ((ItemType _, int count) in player.ItemsCount)
            {
                itemsCount += count;
            }

            if (itemsCount != _itemsCount)
            {
                _itemsCount = itemsCount;

                var args = new ItemsArgs(player.ItemsCount);
                var viewModel = CreateViewModel<ItemsViewModel, ItemsArgs>(args);
                _items.Value = viewModel;
            }
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