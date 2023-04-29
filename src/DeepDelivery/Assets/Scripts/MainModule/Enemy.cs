using System;
using SettingsModule;
using UnityEngine;
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

        public bool HasCollisionWith(Collider2D anotherCollider)
        {
            ColliderDistance2D distance2D = Physics2D.Distance(_enemyBehaviour.HitCollider, anotherCollider);
            return distance2D.isOverlapped;
        }

        public void Dispose()
        {
            Object.Destroy(_enemyBehaviour.gameObject);
        }
    }
}