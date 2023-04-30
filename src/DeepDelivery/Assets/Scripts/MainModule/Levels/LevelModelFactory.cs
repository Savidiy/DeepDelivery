using System.Collections.Generic;
using SettingsModule;
using Object = UnityEngine.Object;

namespace MainModule
{
    public sealed class LevelModelFactory
    {
        private readonly EnemySpawnPointFactory _enemySpawnPointFactory;
        private readonly EnemySpawnUpdater _enemySpawnUpdater;

        public LevelModelFactory(EnemySpawnPointFactory enemySpawnPointFactory, EnemySpawnUpdater enemySpawnUpdater)
        {
            _enemySpawnPointFactory = enemySpawnPointFactory;
            _enemySpawnUpdater = enemySpawnUpdater;
        }

        public LevelModel Create(LevelData levelData)
        {
            LevelBehaviour levelBehaviour = Object.Instantiate(levelData.LevelBehaviour);

            EnemySpawnUpdater enemySpawnUpdater = ResetEnemySpawnUpdater(levelBehaviour);

            var levelModel = new LevelModel(levelBehaviour, enemySpawnUpdater);
            return levelModel;
        }

        private List<EnemySpawnPoint> CreateEnemySpawnPoints(List<EnemySpawnPointBehaviour> enemySpawnPoints)
        {
            var enemies = new List<EnemySpawnPoint>();

            foreach (EnemySpawnPointBehaviour enemySpawnPoint in enemySpawnPoints)
            {
                EnemySpawnPoint enemy = _enemySpawnPointFactory.Create(enemySpawnPoint);
                enemies.Add(enemy);
            }

            return enemies;
        }

        private EnemySpawnUpdater ResetEnemySpawnUpdater(LevelBehaviour levelBehaviour)
        {
            List<EnemySpawnPoint> enemySpawnPoints = CreateEnemySpawnPoints(levelBehaviour.EnemySpawnPoints);
            _enemySpawnUpdater.ClearSpawnPoints();
            _enemySpawnUpdater.AddSpawnPoints(enemySpawnPoints);
            _enemySpawnUpdater.SpawnEnemies();
            return _enemySpawnUpdater;
        }
    }
}