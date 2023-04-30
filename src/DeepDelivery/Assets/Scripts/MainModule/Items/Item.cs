using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Item : IDisposable
    {
        private readonly ItemBehaviour _itemBehaviour;

        public Collider2D Collider => _itemBehaviour.Collider;
        public ItemType ItemType { get; }

        public Item(ItemBehaviour itemBehaviour, ItemType itemType)
        {
            _itemBehaviour = itemBehaviour;
            ItemType = itemType;
        }

        public void Dispose()
        {
            Object.Destroy(_itemBehaviour.gameObject);
        }
    }
}