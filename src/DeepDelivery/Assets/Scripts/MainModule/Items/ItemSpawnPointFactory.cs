using System.Collections.Generic;
using Zenject;

namespace MainModule
{
    public class ItemSpawnPointFactory
    {
        private readonly IInstantiator _instantiator;

        public ItemSpawnPointFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
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
            var point = _instantiator.Instantiate<ItemSpawnPoint>(new object[] {itemSpawnPointBehaviour});
            return point;
        }
    }
}