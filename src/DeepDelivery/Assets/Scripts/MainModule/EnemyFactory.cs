using SettingsModule;
using UnityEngine;

namespace MainModule
{
    public class EnemyFactory
    {
        private readonly EnemyStaticDataProvider _enemyStaticDataProvider;

        public EnemyFactory(EnemyStaticDataProvider enemyStaticDataProvider)
        {
            _enemyStaticDataProvider = enemyStaticDataProvider;
        }

        public Enemy CreateEnemy(EnemySpawnPointBehaviour enemySpawnPoint)
        {
            EnemyType enemyType = enemySpawnPoint.EnemyType;
            EnemyStaticData enemyData = _enemyStaticDataProvider.GetEnemyData(enemyType);
            EnemyBehaviour enemyBehaviour = Object.Instantiate(enemyData.EnemyBehaviour);
            enemyBehaviour.transform.position = enemySpawnPoint.transform.position;

            return new Enemy(enemyBehaviour, enemyData);
        }
    }
}