using System;
using SettingsModule;

namespace MainModule
{
    public sealed class EnemyStaticDataProvider
    {
        private readonly GameStaticData _gameStaticData;

        public EnemyStaticDataProvider(GameStaticData gameStaticData)
        {
            _gameStaticData = gameStaticData;
        }

        public EnemyStaticData GetEnemyData(EnemyType enemyType)
        {
            foreach (EnemyStaticData enemyPrefabData in _gameStaticData.EnemyPrefabs)
            {
                if (enemyPrefabData.EnemyType == enemyType)
                    return enemyPrefabData;
            }

            throw new Exception($"Can't find prefab for enemy type '{enemyType}'");
        }
    }
}