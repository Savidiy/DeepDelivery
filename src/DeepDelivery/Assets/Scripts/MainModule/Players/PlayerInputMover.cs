using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class PlayerInputMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly GameStaticData _gameStaticData;
        private readonly MobileInput _mobileInput;
        private readonly InputStaticData _inputStaticData;

        public PlayerInputMover(TickInvoker tickInvoker, PlayerHolder playerHolder, GameStaticData gameStaticData,
            MobileInput mobileInput, InputStaticData inputStaticData)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _gameStaticData = gameStaticData;
            _mobileInput = mobileInput;
            _inputStaticData = inputStaticData;
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

            if (_inputStaticData.DownKeys.IsAnyKeyPressed()) direction.y -= 1;
            if (_inputStaticData.UpKeys.IsAnyKeyPressed()) direction.y += 1;
            if (_inputStaticData.LeftKeys.IsAnyKeyPressed()) direction.x -= 1;
            if (_inputStaticData.RightKeys.IsAnyKeyPressed()) direction.x += 1;

            direction += _mobileInput.InputDirection;

            if (direction.magnitude > 1)
                direction.Normalize();

            return direction;
        }
    }
}