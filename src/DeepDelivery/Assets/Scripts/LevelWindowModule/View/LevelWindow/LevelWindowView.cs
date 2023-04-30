using System.Collections.Generic;
using MvvmModule;
using UnityEngine;

namespace LevelWindowModule.View
{
    public sealed class LevelWindowView : View<LevelWindowHierarchy, ILevelWindowViewModel>
    {
        private readonly List<HeartHierarchy> _hearts = new();
        private readonly ItemsView _itemsView;

        public LevelWindowView(LevelWindowHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
            _itemsView = CreateView<ItemsView, ItemsHierarchy>(hierarchy.ItemsHierarchy);
#if !UNITY_EDITOR
            hierarchy.RestartLevelButton.gameObject.SetActive(false);
#endif
        }

        protected override void UpdateViewModel(ILevelWindowViewModel viewModel)
        {
            BindClick(Hierarchy.SettingsButton, OnSettingsClick);
            BindClick(Hierarchy.RestartLevelButton, OnRestartLevelClick);

            Bind(viewModel.HeartCount, OnHeartCountChange);
            Bind(viewModel.Items, OnItemsChange);
        }

        private void OnItemsChange(IItemsViewModel itemsViewModel)
        {
            _itemsView.Initialize(itemsViewModel);
        }

        private void OnHeartCountChange(int heartCount)
        {
            for (int i = _hearts.Count - 1; i >= heartCount; i--)
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

        private void OnSettingsClick()
        {
            ViewModel.SettingsClickFromView();
        }

        private void OnRestartLevelClick()
        {
            ViewModel.RestartLevelClickFromView();
        }
    }
}