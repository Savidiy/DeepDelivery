using System;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    [CreateAssetMenu(fileName = "EnemyStaticDataProvider", menuName = "EnemyStaticDataProvider", order = 0)]
    public class EnemyStaticDataProvider : AutoSaveScriptableObject
    {
        public EnemyStaticData[] EnemiesData;
        public float DefaultEnemySpawnCooldown = 5f;
        
        public EnemyStaticData GetEnemyData(EnemyType enemyType)
        {
            foreach (EnemyStaticData enemyPrefabData in EnemiesData)
            {
                if (enemyPrefabData.EnemyType == enemyType)
                    return enemyPrefabData;
            }

            throw new Exception($"Can't find prefab for enemy type '{enemyType}'");
        }
    }

    [Serializable]
    public class EnemyStaticData
    {
        public EnemyType EnemyType;
        public EnemyBehaviour EnemyBehaviour;
        public int HealthPoints = 1;
        public float MoveSpeed = 1f;
    }
}