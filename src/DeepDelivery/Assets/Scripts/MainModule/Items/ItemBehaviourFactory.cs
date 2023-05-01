using UnityEngine;

namespace MainModule
{
    public class ItemBehaviourFactory
    {
        private readonly ItemStaticDataProvider _itemStaticDataProvider;
        private readonly Transform _root;

        public ItemBehaviourFactory(ItemStaticDataProvider itemStaticDataProvider)
        {
            _itemStaticDataProvider = itemStaticDataProvider;
            _root = new GameObject("ItemsRoot").transform;
        }
        
        public ItemBehaviour Create(ItemType itemType, Vector3 position)
        {
            ItemBehaviour prefab = _itemStaticDataProvider.GetData(itemType).Prefab;
            ItemBehaviour itemBehaviour = Object.Instantiate(prefab, _root);
            itemBehaviour.transform.position = position;
            return itemBehaviour;
        }
    }
}