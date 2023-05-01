using System;
using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public sealed class LevelModel : DisposableCollector, IProgressWriter
    {
        private readonly LevelBehaviour _levelBehaviour;
        private readonly EnemySpawnUpdater _enemySpawnUpdater;

        public IReadOnlyList<ItemSpawnPoint> Items { get; }
        public IReadOnlyList<Enemy> Enemies => _enemySpawnUpdater.Enemies;
        public IReadOnlyList<Shop> Shops { get; }
        public IReadOnlyList<QuestGiver> QuestGivers { get; }
        public IReadOnlyList<QuestTaker> QuestTakers { get; }
        public IReadOnlyList<CheckPoint> CheckPoints { get; }

        public LevelModel(LevelBehaviour levelBehaviour, EnemySpawnUpdater enemySpawnUpdater, List<ItemSpawnPoint> items,
            List<Shop> shops,
            List<QuestGiver> questGivers, List<QuestTaker> questTakers, List<CheckPoint> checkPoints)
        {
            _levelBehaviour = levelBehaviour;
            _enemySpawnUpdater = enemySpawnUpdater;
            Items = items;
            Shops = shops;
            QuestGivers = questGivers;
            QuestTakers = questTakers;
            CheckPoints = checkPoints;
        }

        public void LoadProgress(Progress progress)
        {
            LoadProgress(progress, Items);
            LoadProgress(progress, Shops);
            LoadProgress(progress, QuestGivers);
            LoadProgress(progress, QuestTakers);
            LoadProgress(progress, CheckPoints);
        }

        public void UpdateProgress(Progress progress)
        {
            UpdateProgress(progress, Items);
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

            DisposeList(Items);
            DisposeList(Shops);
            DisposeList(QuestGivers);
            DisposeList(QuestTakers);
            DisposeList(CheckPoints);
        }

        private void DisposeList<T>(IReadOnlyList<T> items) where T : class
        {
            foreach (T item in items)
            {
                if (item is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}