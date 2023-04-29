using System.Collections.Generic;
using SettingsModule;
using Object = UnityEngine.Object;

namespace MainModule
{
    public sealed class LevelModelFactory
    {
        private readonly EnemyFactory _enemyFactory;

        public LevelModelFactory(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }
        
        public LevelModel Create(LevelData levelData)
        {
            LevelBehaviour levelBehaviour = Object.Instantiate(levelData.LevelBehaviour);

            List<Enemy> enemies = CreateEnemies(levelBehaviour.EnemySpawnPoints);

            var levelModel = new LevelModel(levelBehaviour, enemies);
            return levelModel;
        }

        private List<Enemy> CreateEnemies(List<EnemySpawnPointBehaviour> enemySpawnPoints)
        {
            var enemies = new List<Enemy>();
            
            foreach (EnemySpawnPointBehaviour enemySpawnPoint in enemySpawnPoints)
            {
                Enemy enemy = _enemyFactory.CreateEnemy(enemySpawnPoint);
                enemies.Add(enemy);
            }

            return enemies;
        }
    }
}