using System;
using SettingsModule;

namespace MainModule
{
    public sealed class EnemyPrefabProvider
    {
        private readonly GameSettings _gameSettings;

        public EnemyPrefabProvider(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }

        public EnemyBehaviour GetEnemyPrefab(EnemyType enemyType)
        {
            foreach (EnemyPrefabData enemyPrefabData in _gameSettings.EnemyPrefabs)
            {
                if (enemyPrefabData.EnemyType == enemyType)
                    return enemyPrefabData.EnemyBehaviour;
            }

            throw new Exception($"Can't find prefab for enemy type '{enemyType}'");
        }
    }
}