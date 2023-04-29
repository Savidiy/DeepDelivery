using UnityEngine;

namespace SettingsModule
{
    public class EnemySpawnPointBehaviour : MonoBehaviour
    {
        public EnemyType EnemyType = EnemyType.Fish;

        private void OnValidate()
        {
            name = $"SpawnPoint: {EnemyType}";
        }
    }
}