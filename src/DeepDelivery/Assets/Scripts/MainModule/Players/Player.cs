using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Player : IDisposable
    {
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly PlayerInvulnerability _playerInvulnerability;

        private bool _isFlipToLeft;

        public Vector3 Position => _playerBehaviour.transform.position;
        public Collider2D Collider => _playerBehaviour.Collider2D;

        public int CurrentHp { get; private set; }
        public int MaxHp { get; private set; }
        public bool IsInvulnerable => _playerInvulnerability.IsInvulnerable;
        public List<Quest> Quests { get; } = new();
        public List<GunType> ActiveGuns { get; set; }
        public Dictionary<ItemType, int> ItemsCount { get; } = new();

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
            MoveBehaviour(shift);
            FlipBehaviour(shift);
        }

        private void MoveBehaviour(Vector2 shift)
        {
            Vector3 position = _playerBehaviour.transform.position;
            position.x += shift.x;
            position.y += shift.y;
            _playerBehaviour.Rigidbody.MovePosition(position);
        }

        private void FlipBehaviour(Vector2 shift)
        {
            if (shift.x < 0)
                _isFlipToLeft = true;
            else if (shift.x > 0)
                _isFlipToLeft = false;

            Vector3 scale = _playerBehaviour.FlipRoot.localScale;
            scale.x = Math.Abs(scale.x) * (_isFlipToLeft ? -1 : 1);
            _playerBehaviour.FlipRoot.localScale = scale;
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

        public Vector3 GetGunDirection(GunType gunType)
        {
            Vector3 direction = GetGun(gunType).ShootDirection;
            if (_isFlipToLeft)
                direction.x = -direction.x;

            return direction;
        }

        public void AddItem(ItemType itemType)
        {
            if (itemType == ItemType.Heart)
            {
                CurrentHp++;
                MaxHp++;
                return;
            }
            
            if (ItemsCount.TryGetValue(itemType, out var count))
                ItemsCount[itemType] = count + 1;
            else
                ItemsCount.Add(itemType, 1);
        }

        public void RemoveItems(ItemType itemType, int count)
        {
            ItemsCount[itemType] -= count;
        }

        public void AddGun(GunType gunType)
        {
            ActiveGuns.Add(gunType);
            UpdateGunVisibility();
        }

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

        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
            Debug.Log($"Took quest. Quest count = {Quests.Count}");
        }

        public void RemoveQuest(Quest quest)
        {
            Quests.Remove(quest);
            CurrentHp = MaxHp;
            Debug.Log($"Complete quest. Quest count = {Quests.Count}");
        }
    }
}