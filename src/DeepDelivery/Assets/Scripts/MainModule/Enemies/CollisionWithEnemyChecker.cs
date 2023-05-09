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
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly Dictionary<Enemy, float> _collidedEnemies = new();

        public CollisionWithEnemyChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, EnemyHolder enemyHolder,
            IAudioPlayer audioPlayer, GameStaticData gameStaticData)
        {
            _enemyHolder = enemyHolder;
            _audioPlayer = audioPlayer;
            _gameStaticData = gameStaticData;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
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
            Player player = _playerHolder.Player;
            Collider2D playerCollider = player.Collider;
            for (var index = 0; index < _enemyHolder.Enemies.Count; index++)
            {
                Enemy enemy = _enemyHolder.Enemies[index];
                bool hasNotCollision = player.IsInvulnerable || !enemy.HasCollisionWith(playerCollider);
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
            Player player = _playerHolder.Player;
            foreach ((Enemy _, float value) in collidedEnemies)
            {
                if (!(value >= _gameStaticData.HurtPlayerDelay))
                    continue;

                player.GetHit();
                _audioPlayer.PlayOnce(player.CurrentHp > 0 ? SoundId.PlayerHurt : SoundId.PlayerDead);
            }
        }
    }
}