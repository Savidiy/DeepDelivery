using System;
using SettingsModule;

namespace MainModule
{
    public sealed class EnemyPrefabProvider
    {
        private readonly GameStaticData _gameStaticData;

        public EnemyPrefabProvider(GameStaticData gameStaticData)
        {
            _gameStaticData = gameStaticData;
        }

        public EnemyBehaviour GetEnemyPrefab(EnemyType enemyType)
        {
            foreach (EnemyPrefabData enemyPrefabData in _gameStaticData.EnemyPrefabs)
            {
                if (enemyPrefabData.EnemyType == enemyType)
                    return enemyPrefabData.EnemyBehaviour;
            }

            throw new Exception($"Can't find prefab for enemy type '{enemyType}'");
        }
    }
}