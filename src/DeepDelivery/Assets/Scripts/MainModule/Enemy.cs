using System;
using SettingsModule;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Enemy : IDisposable
    {
        private readonly EnemyBehaviour _enemyBehaviour;

        public Enemy(EnemyBehaviour enemyBehaviour)
        {
            _enemyBehaviour = enemyBehaviour;
        }

        public void Dispose()
        {
            Object.Destroy(_enemyBehaviour.gameObject);
        }
    }
}