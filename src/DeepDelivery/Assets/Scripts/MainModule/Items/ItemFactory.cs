using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class ItemFactory : IFactory<Item, ItemSpawnPointBehaviour>
    {
        private readonly ItemStaticDataProvider _itemStaticDataProvider;
        private readonly Transform _root;

        public ItemFactory(ItemStaticDataProvider itemStaticDataProvider)
        {
            _itemStaticDataProvider = itemStaticDataProvider;
            _root = new GameObject("ItemsRoot").transform;
        }
        
        public Item Create(ItemSpawnPointBehaviour itemSpawnPointBehaviour)
        {
            ItemType itemType = itemSpawnPointBehaviour.ItemType;
            ItemBehaviour prefab = _itemStaticDataProvider.GetData(itemType).Prefab;
            ItemBehaviour itemBehaviour = Object.Instantiate(prefab, _root);
            itemBehaviour.transform.position = itemSpawnPointBehaviour.transform.position;
            var item = new Item(itemBehaviour, itemType);
            return item;
        }
    }
}