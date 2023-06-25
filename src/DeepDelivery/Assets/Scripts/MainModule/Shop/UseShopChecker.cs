using AudioModule.Contracts;
using Savidiy.Utils;

namespace MainModule
{
    public class UseShopChecker
    {
        private readonly LevelHolder _levelHolder;
        private readonly IAudioPlayer _audioPlayer;
        private readonly TrackedItemsHolder _trackedItemsHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public UseShopChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder,
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
            PlayerVisual playerVisual = _playerHolder.PlayerVisual;

            foreach (Shop shop in _levelHolder.LevelModel.Shops)
            {
                if (shop.CanPlayerBuy(playerVisual))
                {
                    shop.PlayerBuy(playerVisual);
                    _audioPlayer.PlayOnce(SoundId.BuyGun);
                }
                else if(shop.CanPlayerTrack(playerVisual))
                {
                    ItemType itemType = shop.ItemType;
                    _trackedItemsHolder.TrackItem(itemType);
                }
            }
        }
    }
}