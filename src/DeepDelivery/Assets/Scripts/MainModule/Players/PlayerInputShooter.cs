using AudioModule.Contracts;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public sealed class PlayerInputShooter
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly GameStaticData _gameStaticData;
        private readonly InputStaticData _inputStaticData;
        private readonly BulletFactory _bulletFactory;
        private readonly BulletHolder _bulletHolder;
        private readonly MobileInput _mobileInput;
        private readonly IAudioPlayer _audioPlayer;
        private readonly PlayerGunHandler _playerGunHandler;
        private float _cooldownTimer;

        public PlayerInputShooter(TickInvoker tickInvoker, PlayerHolder playerHolder, GameStaticData gameStaticData,
            InputStaticData inputStaticData, BulletFactory bulletFactory, BulletHolder bulletHolder, MobileInput mobileInput,
            IAudioPlayer audioPlayer, PlayerGunHandler playerGunHandler)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _gameStaticData = gameStaticData;
            _inputStaticData = inputStaticData;
            _bulletFactory = bulletFactory;
            _bulletHolder = bulletHolder;
            _mobileInput = mobileInput;
            _audioPlayer = audioPlayer;
            _playerGunHandler = playerGunHandler;
        }

        public void ActivatePlayerControls()
        {
            _tickInvoker.Subscribe(UpdateType.Update, OnUpdated);
        }

        public void DeactivatePlayerControls()
        {
            _tickInvoker.Unsubscribe(UpdateType.Update, OnUpdated);
        }

        private float GetDeltaTime()
        {
            return _tickInvoker.DeltaTime;
        }

        private void OnUpdated()
        {
            UpdateTimer();

            if (CanShoot())
                CreateBullets();
        }

        private void UpdateTimer()
        {
            if (_cooldownTimer > 0)
                _cooldownTimer -= GetDeltaTime();
        }

        private bool CanShoot()
        {
            bool isKeyPressed = _inputStaticData.ShootKeys.IsAnyKeyPressed() || _mobileInput.IsFirePressed;
            return _cooldownTimer <= 0 && isKeyPressed;
        }

        private void CreateBullets()
        {
            _cooldownTimer = _gameStaticData.ShootCooldown;

            PlayerVisual playerVisual = _playerHolder.PlayerVisual;
            if (_playerGunHandler.ActiveGuns.Count == 0)
            {
                _audioPlayer.PlayOnce(SoundId.PlayerEmptyFire);
                return;
            }

            foreach (GunType gunType in _playerGunHandler.ActiveGuns)
            {
                Vector3 gunPosition = playerVisual.GetGunPosition(gunType);
                Vector3 gunDirection = playerVisual.GetGunDirection(gunType);
                Bullet bullet = _bulletFactory.CreateBullet(gunPosition, gunDirection, true);
                _bulletHolder.AddBullet(bullet);
            }

            _audioPlayer.PlayOnce(SoundId.PlayerFire);
        }
    }
}