using SettingsModule;
using UnityEngine;

namespace MainModule
{
    public class EnemyFactory
    {
        private readonly EnemyStaticDataProvider _enemyStaticDataProvider;
        private readonly Transform _root;

        public EnemyFactory(EnemyStaticDataProvider enemyStaticDataProvider)
        {
            _enemyStaticDataProvider = enemyStaticDataProvider;
            _root = new GameObject("EnemyRoot").transform;
        }

        public Enemy Create(EnemySpawnPointBehaviour enemySpawnPoint)
        {
            EnemyType enemyType = enemySpawnPoint.EnemyType;
            EnemyStaticData enemyData = _enemyStaticDataProvider.GetEnemyData(enemyType);
            EnemyBehaviour enemyBehaviour = Object.Instantiate(enemyData.EnemyBehaviour, _root);
            enemyBehaviour.transform.position = enemySpawnPoint.transform.position;

            return new Enemy(enemyBehaviour, enemyData);
        }
    }
}