using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class CollisionWithItemsChecker
    {
        private readonly LevelHolder _levelHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public CollisionWithItemsChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder)
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

            LevelModel levelModel = _levelHolder.LevelModel;

            for (int index = levelModel.Items.Count - 1; index >= 0; index--)
            {
                Item item = levelModel.Items[index];

                if (playerCollider.HasCollisionWith(item.Collider))
                {
                    player.AddItem(item.ItemType);
                    levelModel.RemoveItem(item);
                }
            }
        }
    }
}