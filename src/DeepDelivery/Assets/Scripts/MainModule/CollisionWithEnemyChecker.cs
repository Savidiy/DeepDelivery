using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class CollisionWithEnemyChecker
    {
        private readonly LevelHolder _levelHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public CollisionWithEnemyChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder)
        {
            _levelHolder = levelHolder;
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

        private void OnUpdated()
        {
            Player player = _playerHolder.Player;
            Collider2D playerCollider = player.Collider;
            
            foreach (Enemy enemy in _levelHolder.LevelModel.Enemies)
            {
                if (enemy.HasCollisionWith(playerCollider))
                {
                    Debug.Log("Has collision!");
                }
            }
        }
    }
}