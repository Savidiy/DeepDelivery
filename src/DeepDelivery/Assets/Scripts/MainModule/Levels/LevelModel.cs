using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public sealed class LevelModel : DisposableCollector, IProgressWriter
    {
        private readonly LevelBehaviour _levelBehaviour;
        private readonly EnemySpawnUpdater _enemySpawnUpdater;
        private readonly List<Item> _items;

        public IReadOnlyList<Item> Items => _items;
        public IReadOnlyList<Enemy> Enemies => _enemySpawnUpdater.Enemies;
        public IReadOnlyList<Shop> Shops { get; }
        public IReadOnlyList<QuestGiver> QuestGivers { get; }
        public IReadOnlyList<QuestTaker> QuestTakers { get; }
        public IReadOnlyList<CheckPoint> CheckPoints { get; }

        public LevelModel(LevelBehaviour levelBehaviour, EnemySpawnUpdater enemySpawnUpdater, List<Item> items, List<Shop> shops,
            List<QuestGiver> questGivers, List<QuestTaker> questTakers, List<CheckPoint> checkPoints)
        {
            _levelBehaviour = levelBehaviour;
            _enemySpawnUpdater = enemySpawnUpdater;
            _items = items;
            Shops = shops;
            QuestGivers = questGivers;
            QuestTakers = questTakers;
            CheckPoints = checkPoints;
        }

        public void LoadProgress(Progress progress)
        {
            LoadProgress(progress, _items);
            LoadProgress(progress, Shops);
            LoadProgress(progress, QuestGivers);
            LoadProgress(progress, QuestTakers);
            LoadProgress(progress, CheckPoints);
        }

        public void UpdateProgress(Progress progress)
        {
            UpdateProgress(progress, _items);
            UpdateProgress(progress, Shops);
            UpdateProgress(progress, QuestGivers);
            UpdateProgress(progress, QuestTakers);
            UpdateProgress(progress, CheckPoints);
        }

        private void LoadProgress<T>(Progress progress, IReadOnlyList<T> items) where T : class
        {
            foreach (T item in items)
                if (item is IProgressReader progressReader)
                    progressReader.LoadProgress(progress);
        }

        private void UpdateProgress<T>(Progress progress, IReadOnlyList<T> items) where T : class
        {
            foreach (T item in items)
            {
                if (item is IProgressWriter progressWriter)
                    progressWriter.UpdateProgress(progress);
            }
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
            if (_levelBehaviour != null)
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