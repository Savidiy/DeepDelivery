using Savidiy.Utils;

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
            LevelModel levelModel = _levelHolder.LevelModel;

            foreach (ItemSpawnPoint item in levelModel.Items)
            {
                if (item.CanBeCollect(player))
                    item.Collect(player);
            }
        }
    }
}