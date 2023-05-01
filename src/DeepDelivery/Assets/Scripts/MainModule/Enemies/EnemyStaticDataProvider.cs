using System;
using DG.Tweening;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    [CreateAssetMenu(fileName = "EnemyStaticDataProvider", menuName = "EnemyStaticDataProvider", order = 0)]
    public class EnemyStaticDataProvider : AutoSaveScriptableObject
    {
        public float DefaultEnemySpawnCooldown = 5f;
        public float DestroyEnemyCooldown => EnemyBlinkSettings.EndBlinkDuration + EnemyBlinkSettings.StartBlinkDuration;
        public EnemyBlinkSettings EnemyBlinkSettings; 
        public EnemyStaticData[] EnemiesData;
        
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
    public class EnemyBlinkSettings
    {
        public float StartBlinkDuration = 0.1f;
        public Ease StartEase = Ease.Linear;
        public float EndBlinkDuration = 0.1f;
        public Ease EndEase = Ease.Linear;
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