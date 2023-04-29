using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Bullet : IDisposable
    {
        private readonly BulletBehaviour _bulletBehaviour;
        private readonly Vector3 _direction;

        public bool IsPlayerBullet { get; }
        public Collider2D Collider => _bulletBehaviour.Collider;

        public Bullet(BulletBehaviour bulletBehaviour, Vector3 direction, bool isPlayerBullet)
        {
            IsPlayerBullet = isPlayerBullet;
            _bulletBehaviour = bulletBehaviour;
            _direction = direction;
        }

        public void Move(float delta)
        {
            Vector3 shift = _direction * delta;
            _bulletBehaviour.transform.Translate(shift);
        }

        public void Dispose()
        {
            Object.Destroy(_bulletBehaviour.gameObject);
        }
    }
}