using AudioModule.Contracts;
using Savidiy.Utils;

namespace MainModule
{
    public class CollisionWithItemsChecker
    {
        private readonly LevelHolder _levelHolder;
        private readonly IAudioPlayer _audioPlayer;
        private readonly TrackedItemsHolder _trackedItemsHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public CollisionWithItemsChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder,
            IAudioPlayer audioPlayer, TrackedItemsHolder trackedItemsHolder)
        {
            _levelHolder = levelHolder;
            _audioPlayer = audioPlayer;
            _trackedItemsHolder = trackedItemsHolder;
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
                {
                    item.Collect();
                    _audioPlayer.PlayOnce(SoundId.CollectItem);
                    _trackedItemsHolder.UntrackItem(item.ItemType);
                }
            }
        }
    }
}