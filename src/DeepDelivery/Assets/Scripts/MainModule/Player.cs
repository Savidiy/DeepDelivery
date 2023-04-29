using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Player : IDisposable
    {
        private readonly PlayerBehaviour _playerBehaviour;

        public Vector3 Position => _playerBehaviour.transform.position;
        public Collider2D Collider => _playerBehaviour.Collider2D;

        public Player(PlayerBehaviour playerBehaviour)
        {
            _playerBehaviour = playerBehaviour;
        }

        public void Move(Vector2 shift)
        {
            Vector3 position = _playerBehaviour.transform.position;
            position.x += shift.x;
            position.y += shift.y;
            _playerBehaviour.Rigidbody.MovePosition(position);
        }

        public void SetPosition(Vector3 position)
        {
            _playerBehaviour.transform.position = position;
        }

        public void Dispose()
        {
            Object.Destroy(_playerBehaviour.gameObject);
        }
    }
}