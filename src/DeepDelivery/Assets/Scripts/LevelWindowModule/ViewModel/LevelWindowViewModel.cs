using System.Collections.Generic;
using LevelWindowModule.View;
using MainModule;
using MvvmModule;
using Savidiy.Utils;
using SettingsWindowModule.Contracts;
using UniRx;
using UnityEngine;
using Zenject;

namespace LevelWindowModule
{
    public sealed class LevelWindowViewModel : EmptyViewModel, ILevelWindowViewModel
    {
        private readonly ISettingsWindowPresenter _settingsWindowPresenter;
        private readonly LevelRestarter _playerRestarter;
        private readonly MobileInput _mobileInput;
        private readonly IInstantiator _instantiator;
        private readonly PlayerHealth _playerHealth;
        private readonly PlayerInventory _playerInventory;
        private readonly ReactiveProperty<int> _heartCount = new();
        private readonly ReactiveProperty<IItemsViewModel> _items = new();
        private readonly ReactiveProperty<bool> _isGameCompleted = new();
        private readonly LevelHolder _levelHolder;

        private int _itemsCount = int.MinValue;

        public IReadOnlyReactiveProperty<int> HeartCount => _heartCount;
        public IReadOnlyReactiveProperty<IItemsViewModel> Items => _items;
        public IReadOnlyList<IQuestStatusViewModel> Quests { get; }
        public IReadOnlyReactiveProperty<bool> UseMobileInput { get; }
        public IReadOnlyReactiveProperty<bool> IsGameCompleted => _isGameCompleted;

        public LevelWindowViewModel(ISettingsWindowPresenter settingsWindowPresenter,
            LevelRestarter playerRestarter, TickInvoker tickInvoker, LevelHolder levelHolder,
            MobileInput mobileInput, InputSettings inputSettings, IInstantiator instantiator, PlayerHealth playerHealth,
            PlayerInventory playerInventory)
        {
            _levelHolder = levelHolder;
            _settingsWindowPresenter = settingsWindowPresenter;
            _playerRestarter = playerRestarter;
            _mobileInput = mobileInput;
            _instantiator = instantiator;
            _playerHealth = playerHealth;
            _playerInventory = playerInventory;
            _mobileInput.SetInputDirection(Vector2.zero, false);

            UseMobileInput = inputSettings
                .SelectedControlType
                .Select(a => a == EControlType.Mobile)
                .ToReactiveProperty();

            Quests = CreateQuestStatusViewModels(levelHolder);

            tickInvoker.Updated += OnUpdated;
            OnUpdated();
        }

        private List<QuestStatusViewModel> CreateQuestStatusViewModels(LevelHolder levelHolder)
        {
            List<QuestStatusViewModel> questStatusViewModels = new();
            foreach (QuestGiver questGiver in levelHolder.LevelModel.QuestGivers)
            {
                var args = new QuestStatusArgs(questGiver);
                var viewModel = _instantiator.Instantiate<QuestStatusViewModel>(new object[] {args});
                questStatusViewModels.Add(viewModel);
            }

            return questStatusViewModels;
        }

        private void OnUpdated()
        {
            _heartCount.Value = _playerHealth.CurrentHp;

            UpdateItemsViewModel();
            CheckGameCompleted();
        }

        private void CheckGameCompleted()
        {
            IReadOnlyList<QuestGiver> questGivers = _levelHolder.LevelModel.QuestGivers;

            for (var index = 0; index < questGivers.Count; index++)
            {
                QuestGiver questGiver = questGivers[index];
                if (!questGiver.IsQuestComplete)
                {
                    _isGameCompleted.Value = false;
                    return;
                }
            }

            _isGameCompleted.Value = true;
        }

        private void UpdateItemsViewModel()
        {
            int itemsCount = 0;
            foreach ((ItemType _, int count) in _playerInventory.ItemsCount)
            {
                itemsCount += count;
            }

            if (itemsCount != _itemsCount)
            {
                _itemsCount = itemsCount;

                var args = new ItemsArgs(_playerInventory.ItemsCount);
                var viewModel = _instantiator.Instantiate<ItemsViewModel>(new object[] {args});
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

        public void LoadLevelClickFromView()
        {
            _playerRestarter.LoadLevel();
        }

        public void SetMobileInputFromView(Vector2 inputDirection, bool isFirePressed)
        {
            _mobileInput.SetInputDirection(inputDirection, isFirePressed);
        }
    }
}