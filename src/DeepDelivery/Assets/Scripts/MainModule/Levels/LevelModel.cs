using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public sealed class LevelModel : DisposableCollector
    {
        private readonly LevelBehaviour _levelBehaviour;
        private readonly EnemySpawnUpdater _enemySpawnUpdater;
        private readonly List<Item> _items;

        public IReadOnlyList<Item> Items => _items;
        public IReadOnlyList<Enemy> Enemies => _enemySpawnUpdater.Enemies;
        public IReadOnlyList<Shop> Shops { get; }
        public IReadOnlyList<QuestGiver> QuestGivers { get; }
        public IReadOnlyList<QuestTaker> QuestTakers { get; }

        public LevelModel(LevelBehaviour levelBehaviour, EnemySpawnUpdater enemySpawnUpdater, List<Item> items, List<Shop> shops,
            List<QuestGiver> questGivers, List<QuestTaker> questTakers)
        {
            _items = items;
            Shops = shops;
            QuestGivers = questGivers;
            QuestTakers = questTakers;
            _levelBehaviour = levelBehaviour;
            _enemySpawnUpdater = enemySpawnUpdater;
        }

        public Vector3 GetPlayerStartPosition()
        {
            return _levelBehaviour.StartPoint.position;
        }

        public bool HasCollisionWithWalls(Collider2D collider)
        {
            foreach (Collider2D wall in _levelBehaviour.Walls)
            {
                if (wall.HasCollisionWith(collider))
                {
                    return true;
                }
            }

            return false;
        }

        public override void Dispose()
        {
            base.Dispose();
            Object.Destroy(_levelBehaviour.gameObject);

            foreach (Item item in _items)
                item.Dispose();

            _items.Clear();
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
            item.Dispose();
        }
    }
}