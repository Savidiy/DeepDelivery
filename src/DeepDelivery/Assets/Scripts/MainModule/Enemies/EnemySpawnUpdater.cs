using System;
using System.Collections.Generic;
using Savidiy.Utils;

namespace MainModule
{
    public class EnemySpawnUpdater : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly List<Enemy> _enemies = new();
        private readonly List<EnemySpawnPoint> _enemySpawnPoints = new();

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public EnemySpawnUpdater(TickInvoker tickInvoker)
        {
            _tickInvoker = tickInvoker;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Dispose()
        {
            _tickInvoker.Updated -= OnUpdated;
            ClearSpawnPoints();
        }

        public void ClearSpawnPoints()
        {
            foreach (EnemySpawnPoint enemySpawnPoint in _enemySpawnPoints)
                enemySpawnPoint.Clear();

            _enemySpawnPoints.Clear();
            _enemies.Clear();
        }

        public void AddSpawnPoints(List<EnemySpawnPoint> enemySpawnPoints) =>
            _enemySpawnPoints.AddRange(enemySpawnPoints);

        public void SpawnEnemies()
        {
            foreach (EnemySpawnPoint enemySpawnPoint in _enemySpawnPoints)
            {
                if (enemySpawnPoint.NeedCreateEnemy())
                {
                    Enemy enemy = enemySpawnPoint.SpawnEnemy();
                    _enemies.Add(enemy);
                }
            }
        }

        private void OnUpdated()
        {
            float deltaTime = _tickInvoker.DeltaTime;

            foreach (EnemySpawnPoint enemySpawnPoint in _enemySpawnPoints)
            {
                enemySpawnPoint.UpdateTime(deltaTime);
                if (enemySpawnPoint.NeedCreateEnemy())
                {
                    Enemy enemy = enemySpawnPoint.SpawnEnemy();
                    _enemies.Add(enemy);
                }
                else if (enemySpawnPoint.NeedDestroyEnemy())
                {
                    Enemy enemy = enemySpawnPoint.DestroyEnemy();
                    _enemies.Remove(enemy);
                }
            }
        }
    }
}