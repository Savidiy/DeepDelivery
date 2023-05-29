using System.Collections.Generic;
using LevelWindowModule.View;
using MainModule;
using MvvmModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class ItemsViewModel : ViewModel<ItemsArgs>, IItemsViewModel
    {
        public IReadOnlyList<Sprite> ItemSprites { get; }

        public ItemsViewModel(ItemsArgs model, ItemStaticDataProvider itemStaticDataProvider)
            : base(model)
        {
            List<Sprite> itemSprites = new();
            foreach ((ItemType key, int count) in model.Items)
            {
                Sprite sprite = itemStaticDataProvider.GetData(key).Sprite;
                for (int i = 0; i < count; i++)
                {
                    itemSprites.Add(sprite);
                }
            }

            ItemSprites = itemSprites;
        }
    }
}