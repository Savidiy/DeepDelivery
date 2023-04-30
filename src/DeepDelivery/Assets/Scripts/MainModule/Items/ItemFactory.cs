using System;
using MvvmModule;
using UnityEngine;

namespace MainModule
{
    public class ItemFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly Transform _root;

        public ItemFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
            _root = new GameObject("ItemsRoot").transform;
        }
        
        public Item Create(ItemSpawnPointBehaviour itemSpawnPointBehaviour)
        {
            ItemType itemType = itemSpawnPointBehaviour.ItemType;
            string prefabName = GetPrefabName(itemType);
            var itemBehaviour = _prefabFactory.Instantiate<ItemBehaviour>(prefabName, _root);
            itemBehaviour.transform.position = itemSpawnPointBehaviour.transform.position;
            var item = new Item(itemBehaviour, itemType);
            return item;
        }

        private static string GetPrefabName(ItemType itemType)
        {
            string prefabName = itemType switch
            {
                ItemType.Chest => "ItemChest",
                ItemType.Diamond => "ItemDiamond",
                _ => throw new ArgumentOutOfRangeException()
            };

            return prefabName;
        }
    }
}