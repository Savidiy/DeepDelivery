using AudioModule.Contracts;
using Savidiy.Utils;

namespace MainModule
{
    public class UseShopChecker
    {
        private readonly LevelHolder _levelHolder;
        private readonly IAudioPlayer _audioPlayer;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public UseShopChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder,
            IAudioPlayer audioPlayer)
        {
            _levelHolder = levelHolder;
            _audioPlayer = audioPlayer;
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

            foreach (Shop shop in _levelHolder.LevelModel.Shops)
            {
                if (shop.CanPlayerBuy(player))
                {
                    shop.PlayerBuy(player);
                    _audioPlayer.PlayOnce(SoundId.BuyGun);
                }
            }
        }
    }
}