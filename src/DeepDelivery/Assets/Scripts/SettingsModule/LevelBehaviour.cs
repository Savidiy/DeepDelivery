using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SettingsModule
{
    public class LevelBehaviour : MonoBehaviour
    {
        public Transform StartPoint;
        public List<EnemySpawnPointBehaviour> EnemySpawnPoints;

        [Button]
        private void CollectAllEnemySpawnPoints()
        {
            EnemySpawnPoints.Clear();

            var targets = new List<Transform>() {transform};

            for (var index = 0; index < targets.Count; index++)
            {
                Transform parentTransform = targets[index];
                if (parentTransform.TryGetComponent(out EnemySpawnPointBehaviour spawnPointBehaviour))
                    EnemySpawnPoints.Add(spawnPointBehaviour);

                int childCount = parentTransform.childCount;
                for (int j = 0; j < childCount; j++)
                {
                    targets.Add(parentTransform.GetChild(j));
                }
            }
        }
    }
}