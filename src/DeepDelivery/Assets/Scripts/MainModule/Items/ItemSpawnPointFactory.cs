using System.Collections.Generic;

namespace MainModule
{
    public class ItemSpawnPointFactory
    {
        private readonly ItemBehaviourFactory _itemBehaviourFactory;

        public ItemSpawnPointFactory(ItemBehaviourFactory itemBehaviourFactory)
        {
            _itemBehaviourFactory = itemBehaviourFactory;
        }

        public List<ItemSpawnPoint> CreatePoints(List<ItemSpawnPointBehaviour> behaviours)
        {
            var itemSpawnPoints = new List<ItemSpawnPoint>();
            foreach (ItemSpawnPointBehaviour behaviour in behaviours)
                itemSpawnPoints.Add(Create(behaviour));

            return itemSpawnPoints;
        }

        private ItemSpawnPoint Create(ItemSpawnPointBehaviour itemSpawnPointBehaviour)
        {
            var item = new ItemSpawnPoint(itemSpawnPointBehaviour, _itemBehaviourFactory);
            return item;
        }
    }
}