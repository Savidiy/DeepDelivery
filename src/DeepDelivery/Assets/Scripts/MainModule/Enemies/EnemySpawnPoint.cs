using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

namespace MainModule
{
    public class EnemySpawnPoint : IDisposable
    {
        private readonly EnemySpawnPointBehaviour _behaviour;
        private readonly EnemyFactory _enemyFactory;
        private readonly CameraProvider _cameraProvider;
        private readonly EnemyStaticDataProvider _enemyStaticDataProvider;

        private float _timer;
        private bool _enemyWasCreated;
        [CanBeNull] private Enemy _enemy;
        [CanBeNull] private Sequence _destroyTween;

        public string Id => _behaviour.UniqueId.Id;

        public EnemySpawnPoint(EnemySpawnPointBehaviour behaviour, EnemyFactory enemyFactory,
            CameraProvider cameraProvider, EnemyStaticDataProvider enemyStaticDataProvider)
        {
            _behaviour = behaviour;
            _enemyFactory = enemyFactory;
            _cameraProvider = cameraProvider;
            _enemyStaticDataProvider = enemyStaticDataProvider;
        }

        public void UpdateTime(float deltaTime)
        {
            if (CanUpdateTimer())
                _timer -= deltaTime;

            _behaviour.SetTimerInfo(_timer);
        }

        private bool CanUpdateTimer()
        {
            if (_timer <= 0)
                return false;

            if (_enemy != null)
                return false;

            if (_behaviour.RespawnType == RespawnType.ByTimer)
                return true;

            if (_behaviour.RespawnType == RespawnType.ByTimerWhenInvisible && IsPointInvisibleForCamera())
                return true;

            return false;
        }

        private bool IsPointInvisibleForCamera()
        {
            Camera camera = _cameraProvider.Camera;
            Vector3 position = _behaviour.transform.position;

            Vector3 viewportPoint = camera.WorldToViewportPoint(position);
            bool isVisible = viewportPoint.x is >= 0 and <= 1 && viewportPoint.y is >= 0 and <= 1;
            return !isVisible;
        }

        public bool NeedCreateEnemy()
        {
            if (_enemy != null)
                return false;

            switch (_behaviour.RespawnType)
            {
                case RespawnType.None:
                    return !_enemyWasCreated;
                case RespawnType.ByTimer:
                case RespawnType.ByTimerWhenInvisible:
                    return _timer <= 0;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Enemy SpawnEnemy()
        {
            if (_enemy != null)
                throw new Exception("There is enemy! Can't spawn new");

            _enemyWasCreated = true;
            RestartTimer();

            Enemy enemy = _enemyFactory.Create(_behaviour);
            _enemy = enemy;
            return enemy;
        }

        public bool NeedDestroyEnemy()
        {
            if (_enemy == null)
                return false;

            return _enemy.Hp <= 0;
        }

        public Enemy DestroyEnemyWithDelay()
        {
            if (_enemy == null)
                throw new Exception("Can't destroy enemy! There is not enemy");

            Enemy enemy = _enemy;
            _enemy = null;

            DestroyEnemyWithDelay(enemy);
            return enemy;
        }

        private void DestroyEnemyWithDelay(Enemy enemy)
        {
            _destroyTween?.Kill();
            _destroyTween = DOTween.Sequence()
                .AppendInterval(_enemyStaticDataProvider.DestroyEnemyCooldown)
                .AppendCallback(enemy.Dispose);
        }

        public void Dispose()
        {
            _enemy?.Dispose();
            _destroyTween?.Kill();
        }

        private void RestartTimer()
        {
            if (_behaviour.RespawnType == RespawnType.None)
                return;

            _timer = _behaviour.UseCustomTimerDuration
                ? _behaviour.CustomTimerDuration
                : _enemyStaticDataProvider.DefaultEnemySpawnCooldown;
        }

        public EnemySpawnPointProgress CreateProgress()
        {
            bool enemyWasCreated = _enemyWasCreated;
            float timer = _timer;

            bool hasEnemy = _enemy != null;
            Vector3 enemyPosition = hasEnemy ? _enemy.Position : Vector3.zero;
            Quaternion enemyRotation = hasEnemy ? _enemy.Rotation : Quaternion.identity;
            int enemyHp = hasEnemy ? _enemy.Hp : 0;
            EnemyMoveProgress enemyMoveProgress = hasEnemy ? _enemy.EnemyMover.GetProgress() : new EnemyMoveProgress();

            var progress = new EnemySpawnPointProgress(Id, hasEnemy, enemyWasCreated, timer, enemyPosition, enemyRotation, enemyHp,
                enemyMoveProgress);

            return progress;
        }

        public bool LoadProgressWithSpawn(EnemySpawnPointProgress progress, out Enemy enemy)
        {
            _enemy?.Dispose();
            _destroyTween?.Kill();
            enemy = null;

            _timer = progress.Timer;
            _enemyWasCreated = progress.EnemyWasCreated;

            if (!progress.HasEnemy)
                return false;

            enemy = SpawnEnemy();
            enemy.LoadProgress(progress);
            _enemy = enemy;
            return true;
        }
    }
}