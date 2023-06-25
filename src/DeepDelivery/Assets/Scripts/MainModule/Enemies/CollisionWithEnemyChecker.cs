using System.Collections.Generic;
using AudioModule.Contracts;
using Savidiy.Utils;
using UnityEngine;
using UnityEngine.Pool;

namespace MainModule
{
    public class CollisionWithEnemyChecker
    {
        private readonly EnemyHolder _enemyHolder;
        private readonly IAudioPlayer _audioPlayer;
        private readonly GameStaticData _gameStaticData;
        private readonly PlayerInvulnerability _playerInvulnerability;
        private readonly PlayerHealth _playerHealth;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly Dictionary<Enemy, float> _collidedEnemies = new();

        public CollisionWithEnemyChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, EnemyHolder enemyHolder,
            IAudioPlayer audioPlayer, GameStaticData gameStaticData, PlayerInvulnerability playerInvulnerability, PlayerHealth playerHealth)
        {
            _enemyHolder = enemyHolder;
            _audioPlayer = audioPlayer;
            _gameStaticData = gameStaticData;
            _playerInvulnerability = playerInvulnerability;
            _playerHealth = playerHealth;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
        }

        public void Activate()
        {
            _tickInvoker.Subscribe(UpdateType.Update, OnUpdated);
        }

        public void Deactivate()
        {
            _tickInvoker.Unsubscribe(UpdateType.Update, OnUpdated);
        }

        public void ClearCollisions()
        {
            _collidedEnemies.Clear();
        }

        private void OnUpdated()
        {
            UpdateCollidedEnemiesCollection(_collidedEnemies);
            UpdateCollidedEnemiesTimers(_collidedEnemies);
            HitPlayer(_collidedEnemies);
        }

        private void UpdateCollidedEnemiesCollection(Dictionary<Enemy, float> collidedEnemies)
        {
            PlayerVisual playerVisual = _playerHolder.PlayerVisual;
            Collider2D playerCollider = playerVisual.Collider;
            for (var index = 0; index < _enemyHolder.Enemies.Count; index++)
            {
                Enemy enemy = _enemyHolder.Enemies[index];
                bool hasNotCollision = _playerInvulnerability.IsInvulnerable || !enemy.HasCollisionWith(playerCollider);
                if (hasNotCollision)
                {
                    if (collidedEnemies.ContainsKey(enemy))
                        collidedEnemies.Remove(enemy);
                }
                else
                {
                    collidedEnemies.TryAdd(enemy, 0f);
                }
            }
        }

        private void UpdateCollidedEnemiesTimers(Dictionary<Enemy, float> collidedEnemies)
        {
            float deltaTime = _tickInvoker.DeltaTime;

            List<Enemy> enemies = ListPool<Enemy>.Get();
            enemies.AddRange(collidedEnemies.Keys);

            foreach (Enemy key in enemies)
                collidedEnemies[key] += deltaTime;

            ListPool<Enemy>.Release(enemies);
        }

        private void HitPlayer(Dictionary<Enemy, float> collidedEnemies)
        {
            foreach ((Enemy _, float value) in collidedEnemies)
            {
                if (!(value >= _gameStaticData.HurtPlayerDelay))
                    continue;

                _playerHealth.GetHit();
                _playerInvulnerability.StartInvulnerableTimer();
                _audioPlayer.PlayOnce(_playerHealth.CurrentHp > 0 ? SoundId.PlayerHurt : SoundId.PlayerDead);
            }
        }
    }
}