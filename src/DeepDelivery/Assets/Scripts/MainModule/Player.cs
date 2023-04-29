using System;
using System.Collections.Generic;
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
        public List<GunType> ActiveGuns { get; set; }

        public Player(PlayerBehaviour playerBehaviour, ProgressProvider progressProvider,
            PlayerInvulnerability playerInvulnerability)
        {
            _playerBehaviour = playerBehaviour;
            _playerInvulnerability = playerInvulnerability;
            Progress progress = progressProvider.Progress;
            CurrentHp = progress.CurrentHp;
            MaxHp = progress.MaxHp;
            ActiveGuns = new List<GunType>(progress.ActiveGuns);

            UpdateGunVisibility();
        }

        private void UpdateGunVisibility()
        {
            _playerBehaviour.TopGun.SetActive(ActiveGuns.Contains(GunType.TopGun));
            _playerBehaviour.BottomGun.SetActive(ActiveGuns.Contains(GunType.BottomGun));
            _playerBehaviour.ForwardGun.SetActive(ActiveGuns.Contains(GunType.ForwardGun));
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

        public Vector3 GetGunPosition(GunType gunType) => GetGun(gunType).BulletSpawnPoint.position;
        public Vector3 GetGunDirection(GunType gunType) => GetGun(gunType).ShootDirection;

        private GunBehaviour GetGun(GunType gunType)
        {
            return gunType switch
            {
                GunType.TopGun => _playerBehaviour.TopGun,
                GunType.BottomGun => _playerBehaviour.BottomGun,
                GunType.ForwardGun => _playerBehaviour.ForwardGun,
                _ => throw new ArgumentOutOfRangeException(nameof(gunType), gunType, null)
            };
        }
    }
}