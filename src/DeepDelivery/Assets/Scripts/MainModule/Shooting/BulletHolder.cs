using System;
using System.Collections.Generic;
using Savidiy.Utils;
using SettingsModule;

namespace MainModule
{
    public class BulletHolder : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly LevelHolder _levelHolder;
        private readonly GameStaticData _gameStaticData;
        private readonly List<Bullet> _bullets = new();
        private readonly List<Bullet> _needRemoveBullets = new();

        public BulletHolder(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder,
            GameStaticData gameStaticData)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _levelHolder = levelHolder;
            _gameStaticData = gameStaticData;
            _tickInvoker.Updated += OnUpdated;
        }

        public void AddBullet(Bullet bullet) => _bullets.Add(bullet);

        public void Dispose()
        {
            ClearBullets();
            _tickInvoker.Updated -= OnUpdated;
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
                else
                    CheckCollisionWithPlayer(bullet);
            }
        }

        private void CheckCollisionWithPlayer(Bullet bullet)
        {
            throw new NotImplementedException();
        }

        private void CheckCollisionWithEnemies(Bullet bullet)
        {
            IReadOnlyList<Enemy> enemies = _levelHolder.LevelModel.Enemies;
            foreach (Enemy enemy in enemies)
            {
                if (enemy.HasCollisionWith(bullet.Collider))
                {
                    _needRemoveBullets.Add(bullet);
                    enemy.GetHit();
                    break;
                }
            }
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