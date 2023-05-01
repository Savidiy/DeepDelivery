using System.Collections.Generic;
using Savidiy.Utils;

namespace MainModule
{
    public class EnemySpawnUpdater : IProgressWriter
    {
        private readonly TickInvoker _tickInvoker;
        private readonly LevelHolder _levelHolder;
        private readonly EnemyHolder _enemyHolder;

        private IReadOnlyList<EnemySpawnPoint> EnemySpawnPoints => _levelHolder.LevelModel.EnemySpawnPoints;

        public EnemySpawnUpdater(TickInvoker tickInvoker, ProgressUpdater progressUpdater, LevelHolder levelHolder,
            EnemyHolder enemyHolder)
        {
            _tickInvoker = tickInvoker;
            _levelHolder = levelHolder;
            _enemyHolder = enemyHolder;

            progressUpdater.Register(this);
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        public void UpdateProgress(Progress progress)
        {
            progress.EnemySpawnPoints = new();

            foreach (EnemySpawnPoint spawnPoint in EnemySpawnPoints)
            {
                EnemySpawnPointProgress spawnPointProgress = spawnPoint.CreateProgress();
                progress.EnemySpawnPoints.Add(spawnPointProgress);
            }
        }

        public void LoadProgress(Progress progress)
        {
            _enemyHolder.Clear();

            foreach (EnemySpawnPoint spawnPoint in EnemySpawnPoints)
            {
                if (HasProgress(progress, spawnPoint, out EnemySpawnPointProgress spawnPointProgress))
                {
                    if (spawnPoint.LoadProgressWithSpawn(spawnPointProgress, out Enemy enemy))
                        _enemyHolder.Add(enemy);
                }
                else
                {
                    TrySpawnEnemy(spawnPoint);
                }
            }
        }

        private static bool HasProgress(Progress progress, EnemySpawnPoint spawnPoint,
            out EnemySpawnPointProgress spawnPointProgress)
        {
            spawnPointProgress = null;
            if (progress.EnemySpawnPoints == null)
                return false;

            foreach (EnemySpawnPointProgress pointProgress in progress.EnemySpawnPoints)
            {
                if (pointProgress.Id.Equals(spawnPoint.Id))
                {
                    spawnPointProgress = pointProgress;
                    return true;
                }
            }

            return false;
        }

        private void TrySpawnEnemy(EnemySpawnPoint enemySpawnPoint)
        {
            if (enemySpawnPoint.NeedCreateEnemy())
            {
                Enemy enemy = enemySpawnPoint.SpawnEnemy();
                _enemyHolder.Add(enemy);
            }
        }

        private void OnUpdated()
        {
            float deltaTime = _tickInvoker.DeltaTime;

            foreach (EnemySpawnPoint enemySpawnPoint in EnemySpawnPoints)
            {
                enemySpawnPoint.UpdateTime(deltaTime);
                if (enemySpawnPoint.NeedCreateEnemy())
                {
                    Enemy enemy = enemySpawnPoint.SpawnEnemy();
                    _enemyHolder.Add(enemy);
                }
                else if (enemySpawnPoint.NeedDestroyEnemy())
                {
                    Enemy enemy = enemySpawnPoint.DestroyEnemyWithDelay();
                    _enemyHolder.Remove(enemy);
                }
            }
        }
    }
}