using System.Collections.Generic;
using MvvmModule;
using UnityEngine;

namespace LevelWindowModule.View
{
    public sealed class ItemsView : View<ItemsHierarchy, IItemsViewModel>
    {
        private readonly List<ItemHierarchy> _itemViews = new();
        
        public ItemsView(ItemsHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(IItemsViewModel viewModel)
        {
            ClearItemsViews();
            foreach (Sprite sprite in viewModel.ItemSprites)
            {
                ItemHierarchy itemHierarchy = Object.Instantiate(Hierarchy.ItemPrefab, Hierarchy.ItemsRoot);
                itemHierarchy.Image.sprite = sprite;
                _itemViews.Add(itemHierarchy);
            }
        }

        private void ClearItemsViews()
        {
            foreach (ItemHierarchy itemHierarchy in _itemViews)
                Object.Destroy(itemHierarchy.gameObject);

            _itemViews.Clear();
        }
    }
}