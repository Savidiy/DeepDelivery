using UnityEngine;

namespace MainModule
{
    public class Player
    {
        private readonly PlayerBehaviour _playerBehaviour;

        public Player(PlayerBehaviour playerBehaviour)
        {
            _playerBehaviour = playerBehaviour;
        }

        public void Move(Vector2 shift)
        {
            Vector3 position = _playerBehaviour.transform.position;
            position.x += shift.x;
            position.y += shift.y;
            _playerBehaviour.Rigidbody.MovePosition(position);
        }
    }
}