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

        public LevelModel(LevelBehaviour levelBehaviour, EnemySpawnUpdater enemySpawnUpdater, List<Item> items)
        {
            _items = items;
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
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
            item.Dispose();
        }
    }
}