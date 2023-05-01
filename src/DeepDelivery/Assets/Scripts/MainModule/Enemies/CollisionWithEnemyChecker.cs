using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class CollisionWithEnemyChecker
    {
        private readonly EnemyHolder _enemyHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public CollisionWithEnemyChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, EnemyHolder enemyHolder)
        {
            _enemyHolder = enemyHolder;
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
            
            foreach (Enemy enemy in _enemyHolder.Enemies)
            {
                if (player.IsInvulnerable)
                    break;
                
                if (enemy.HasCollisionWith(playerCollider))
                    player.GetHit();
            }
        }
    }
}