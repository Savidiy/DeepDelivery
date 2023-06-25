using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class PlayerVisual : IProgressWriter, IDisposable
    {
        private readonly PlayerBehaviour _playerBehaviour;
        private readonly PlayerGunHandler _gunHandler;
        private readonly LevelHolder _levelHolder;
        private readonly ProgressUpdater _progressUpdater;
        private bool _isFlipToLeft;

        public Vector3 Position => _playerBehaviour.transform.position;
        public Collider2D Collider => _playerBehaviour.Collider2D;

        public PlayerVisual(PlayerBehaviour playerBehaviour, PlayerGunHandler gunHandler, LevelHolder levelHolder, 
            ProgressUpdater progressUpdater)
        {
            _progressUpdater = progressUpdater;
            _playerBehaviour = playerBehaviour;
            _gunHandler = gunHandler;
            _levelHolder = levelHolder;
            
            progressUpdater.Register(this);
        }

        public void LoadProgress(Progress progress)
        {
            Vector3 defaultPosition = _levelHolder.LevelModel.GetPlayerStartPosition();
            
            Vector3 position = progress.Player.HasSavedPosition
                ? progress.Player.SavedPosition.ToVector3()
                : defaultPosition;

            _playerBehaviour.transform.position = position;

            UpdateGunVisibility();
        }

        public void UpdateProgress(Progress progress)
        {
            progress.Player.HasSavedPosition = true;
            progress.Player.SavedPosition = new SerializableVector3(Position);
        }

        public void UpdateGunVisibility()
        {
            _playerBehaviour.TopGun.SetActive(_gunHandler.HasGun(GunType.TopGun));
            _playerBehaviour.BottomGun.SetActive(_gunHandler.HasGun(GunType.BottomGun));
            _playerBehaviour.ForwardGun.SetActive(_gunHandler.HasGun(GunType.ForwardGun));
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

        public void Dispose()
        {
            Object.Destroy(_playerBehaviour.gameObject);
            _progressUpdater.Unregister(this);
        }

        public Vector3 GetGunPosition(GunType gunType) =>
            GetGun(gunType).BulletSpawnPoint.position;

        public Vector3 GetGunDirection(GunType gunType)
        {
            Vector3 direction = GetGun(gunType).ShootDirection;
            if (_isFlipToLeft)
                direction.x = -direction.x;

            return direction;
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
    }
}