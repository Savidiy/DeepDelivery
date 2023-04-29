using SettingsModule;
using UnityEngine;

namespace MainModule
{
    public class EnemyFactory
    {
        private readonly EnemyPrefabProvider _enemyPrefabProvider;

        public EnemyFactory(EnemyPrefabProvider enemyPrefabProvider)
        {
            _enemyPrefabProvider = enemyPrefabProvider;
        }
        
        public Enemy CreateEnemy(EnemySpawnPointBehaviour enemySpawnPoint)
        {
            EnemyType enemyType = enemySpawnPoint.EnemyType;
            EnemyBehaviour prefab = _enemyPrefabProvider.GetEnemyPrefab(enemyType);
            EnemyBehaviour enemyBehaviour = Object.Instantiate(prefab);
            enemyBehaviour.name = enemyType.ToString();
            enemyBehaviour.transform.position = enemySpawnPoint.transform.position;
            return new Enemy(enemyBehaviour);
        }
    }
}