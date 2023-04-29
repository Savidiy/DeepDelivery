using Savidiy.Utils;
using SettingsModule;
using UnityEngine;

namespace MainModule
{
    public class PlayerInputMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly GameSettings _gameSettings;

        public PlayerInputMover(TickInvoker tickInvoker, PlayerHolder playerHolder, GameSettings gameSettings)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _gameSettings = gameSettings;
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
            shift.x *= _gameSettings.PlayerSpeedX;
            shift.y *= _gameSettings.PlayerSpeedY;
            return shift;
        }

        private Vector2 GetInputDirection()
        {
            Vector2 direction = Vector2.zero;

            if (IsAnyKeyPressed(_gameSettings.DownKeys)) direction.y -= 1;
            if (IsAnyKeyPressed(_gameSettings.UpKeys)) direction.y += 1;
            if (IsAnyKeyPressed(_gameSettings.LeftKeys)) direction.x -= 1;
            if (IsAnyKeyPressed(_gameSettings.RightKeys)) direction.x += 1;

            direction.Normalize();

            return direction;
        }

        private bool IsAnyKeyPressed(KeyCode[] keyCodes)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                    return true;
            }

            return false;
        }
    }
}