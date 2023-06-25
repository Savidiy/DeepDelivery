using System;
using System.Collections.Generic;
using AudioModule.Contracts;
using Savidiy.Utils;

namespace MainModule
{
    public class BulletHolder : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly EnemyHolder _enemyHolder;
        private readonly LevelHolder _levelHolder;
        private readonly GameStaticData _gameStaticData;
        private readonly IAudioPlayer _audioPlayer;
        private readonly List<Bullet> _bullets = new();
        private readonly List<Bullet> _needRemoveBullets = new();

        public BulletHolder(TickInvoker tickInvoker, EnemyHolder enemyHolder, LevelHolder levelHolder,
            GameStaticData gameStaticData, IAudioPlayer audioPlayer)
        {
            _tickInvoker = tickInvoker;
            _enemyHolder = enemyHolder;
            _levelHolder = levelHolder;
            _gameStaticData = gameStaticData;
            _audioPlayer = audioPlayer;
            _tickInvoker.Subscribe(UpdateType.Update, OnUpdated);
        }

        public void AddBullet(Bullet bullet) => _bullets.Add(bullet);

        public void Dispose()
        {
            ClearBullets();
            _tickInvoker.Unsubscribe(UpdateType.Update, OnUpdated);
        }

        public void ClearBullets()
        {
            foreach (Bullet bullet in _bullets)
            {
                bullet.Dispose();
            }

            _bullets.Clear();
        }

        private void OnUpdated()
        {
            MoveBullets();
            CheckCollisions();
            CheckCollisionWithWalls();
            RemoveUsedBullets();
        }

        private void MoveBullets()
        {
            float speed = _gameStaticData.PlayerBulletSpeed;
            float deltaTime = _tickInvoker.DeltaTime;
            float delta = speed * deltaTime;

            foreach (Bullet bullet in _bullets)
                bullet.Move(delta);
        }

        private void CheckCollisions()
        {
            foreach (Bullet bullet in _bullets)
            {
                if (bullet.IsPlayerBullet)
                    CheckCollisionWithEnemies(bullet);
            }
        }

        private void CheckCollisionWithEnemies(Bullet bullet)
        {
            IReadOnlyList<Enemy> enemies = _enemyHolder.Enemies;
            foreach (Enemy enemy in enemies)
            {
                if (enemy.HasCollisionWith(bullet.Collider))
                {
                    _needRemoveBullets.Add(bullet);
                    enemy.GetHit();
                    PlayHitSound(enemy.EnemyType);
                    break;
                }
            }
        }

        private void PlayHitSound(EnemyType enemyType)
        {
            SoundId soundId = enemyType switch
            {
                EnemyType.Fish => SoundId.HurtFish,
                EnemyType.Octopus => SoundId.HurtOctopus,
                EnemyType.Wall => SoundId.HurtStone,
                EnemyType.Coral => SoundId.HurtCoral,
                EnemyType.Stone1 => SoundId.HurtStone,
                EnemyType.Stone2 =>  SoundId.HurtStone,
                EnemyType.Stone3 =>  SoundId.HurtStone,
                _ => SoundId.HurtStone
            };
            
            _audioPlayer.PlayOnce(soundId);
        }

        private void CheckCollisionWithWalls()
        {
            LevelModel levelModel = _levelHolder.LevelModel;
            foreach (Bullet bullet in _bullets)
            {
                if (levelModel.HasCollisionWithWalls(bullet.Collider))
                {
                    _needRemoveBullets.Add(bullet);
                }
            }
        }

        private void RemoveUsedBullets()
        {
            foreach (Bullet bullet in _needRemoveBullets)
            {
                bullet.Dispose();
                _bullets.Remove(bullet);
            }

            _needRemoveBullets.Clear();
        }
    }
}