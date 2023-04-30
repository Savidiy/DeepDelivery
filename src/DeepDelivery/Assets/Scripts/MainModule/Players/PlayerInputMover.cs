using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace MainModule
{
    public class PlayerInputMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly GameStaticData _gameStaticData;

        public PlayerInputMover(TickInvoker tickInvoker, PlayerHolder playerHolder, GameStaticData gameStaticData)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _gameStaticData = gameStaticData;
        }

        public void ActivatePlayerControls()
        {
            _tickInvoker.FixedUpdated -= OnUpdated;
            _tickInvoker.FixedUpdated += OnUpdated;
        }

        public void DeactivatePlayerControls()
        {
            _tickInvoker.FixedUpdated -= OnUpdated;
        }

        private float GetDeltaTime()
        {
            return _tickInvoker.FixedDeltaTime;
        }

        private void OnUpdated()
        {
            Vector2 shift = CalcMoveShift();
            Player player = _playerHolder.Player;
            player.Move(shift);
        }

        private Vector2 CalcMoveShift()
        {
            Vector2 inputDirection = GetInputDirection();
            
            float deltaTime = GetDeltaTime();
            Vector2 shift = inputDirection * deltaTime;
            shift.x *= _gameStaticData.PlayerSpeedX;
            shift.y *= _gameStaticData.PlayerSpeedY;
            return shift;
        }

        private Vector2 GetInputDirection()
        {
            Vector2 direction = Vector2.zero;

            if (_gameStaticData.DownKeys.IsAnyKeyPressed()) direction.y -= 1;
            if (_gameStaticData.UpKeys.IsAnyKeyPressed()) direction.y += 1;
            if (_gameStaticData.LeftKeys.IsAnyKeyPressed()) direction.x -= 1;
            if (_gameStaticData.RightKeys.IsAnyKeyPressed()) direction.x += 1;

            direction.Normalize();

            return direction;
        }
    }
}