using System;
using ProgressModule;
using SettingsModule;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Player : IDisposable
    {
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly PlayerInvulnerability _playerInvulnerability;

        public Vector3 Position => _playerBehaviour.transform.position;
        public Collider2D Collider => _playerBehaviour.Collider2D;

        public int CurrentHp { get; private set; }
        public int MaxHp { get; private set; }
        public bool IsInvulnerable => _playerInvulnerability.IsInvulnerable;

        public Player(PlayerBehaviour playerBehaviour, ProgressProvider progressProvider,
            PlayerInvulnerability playerInvulnerability)
        {
            _playerBehaviour = playerBehaviour;
            _playerInvulnerability = playerInvulnerability;
            Progress progress = progressProvider.Progress;
            CurrentHp = progress.CurrentHp;
            MaxHp = progress.MaxHp;
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

        public void GetHit()
        {
            CurrentHp--;
            _playerInvulnerability.StartInvulnerableTimer();
        }

        public void Dispose()
        {
            Object.Destroy(_playerBehaviour.gameObject);
        }
    }
}