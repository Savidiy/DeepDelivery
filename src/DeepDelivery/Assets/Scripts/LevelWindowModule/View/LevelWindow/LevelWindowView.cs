using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using UnityEngine;

namespace LevelWindowModule.View
{
    public sealed class LevelWindowView : View<LevelWindowHierarchy, ILevelWindowViewModel>
    {
        private readonly TickInvoker _tickInvoker;
        private readonly List<HeartHierarchy> _hearts = new();
        private readonly List<QuestStatusView> _questStatusViews = new();
        private readonly ItemsView _itemsView;

        private const string PREFAB_NAME = "QuestStatus";

        public LevelWindowView(LevelWindowHierarchy hierarchy, IViewFactory viewFactory, TickInvoker tickInvoker) : base(hierarchy, viewFactory)
        {
            _tickInvoker = tickInvoker;
            _itemsView = CreateView<ItemsView, ItemsHierarchy>(hierarchy.ItemsHierarchy);

#if !UNITY_EDITOR
            hierarchy.RestartLevelButton.gameObject.SetActive(false);
            hierarchy.LoadLevelButton.gameObject.SetActive(false);
#endif
        }

        protected override void UpdateViewModel(ILevelWindowViewModel viewModel)
        {
            BindClick(Hierarchy.SettingsButton, OnSettingsClick);
            BindClick(Hierarchy.RestartLevelButton, OnRestartLevelClick);
            BindClick(Hierarchy.LoadLevelButton, OnLoadLevelClick);

            Bind(viewModel.HeartCount, OnHeartCountChange);
            Bind(viewModel.Items, OnItemsChange);
            Bind(viewModel.UseMobileInput, OnUseMobileInputChange);
            Bind(viewModel.IsGameCompleted, OnIsGameCompletedChange);

            UpdateQuestsViews(viewModel);
            
            _tickInvoker.Updated += OnUpdated;
        }

        private void OnIsGameCompletedChange(bool isGameCompleted) => Hierarchy.CompletedGame.SetActive(isGameCompleted);

        private void OnUseMobileInputChange(bool useMobileInput) => Hierarchy.MobileInput.SetActive(useMobileInput);

        protected override void ReleaseViewModel()
        {
            base.ReleaseViewModel();
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            ViewModel.SetMobileInputFromView(Hierarchy.MoveStick.InputDirection, Hierarchy.FireButton.IsPressed.Value);
        }

        private void UpdateQuestsViews(ILevelWindowViewModel viewModel)
        {
            foreach (QuestStatusView questStatusView in _questStatusViews)
                questStatusView.Dispose();

            _questStatusViews.Clear();

            foreach (IQuestStatusViewModel questStatusViewModel in viewModel.Quests)
            {
                var questStatus = CreateView<QuestStatusView, QuestStatusHierarchy>(PREFAB_NAME, Hierarchy.QuestsRoot);
                questStatus.Initialize(questStatusViewModel);
                _questStatusViews.Add(questStatus);
            }
        }

        private void OnItemsChange(IItemsViewModel itemsViewModel)
        {
            _itemsView.Initialize(itemsViewModel);
        }

        private void OnHeartCountChange(int heartCount)
        {
            for (int i = _hearts.Count - 1; i >= heartCount && i >= 0; i--)
            {
                HeartHierarchy heartHierarchy = _hearts[i];
                Object.Destroy(heartHierarchy.gameObject);
                _hearts.RemoveAt(i);
            }

            for (int i = _hearts.Count; i < heartCount; i++)
            {
                HeartHierarchy heartHierarchy = Object.Instantiate(Hierarchy.HeartPrefab, Hierarchy.HeartRoot);
                _hearts.Add(heartHierarchy);
            }
        }

        private void OnSettingsClick() => ViewModel.SettingsClickFromView();
        private void OnRestartLevelClick() => ViewModel.RestartLevelClickFromView();
        private void OnLoadLevelClick() => ViewModel.LoadLevelClickFromView();
    }
}