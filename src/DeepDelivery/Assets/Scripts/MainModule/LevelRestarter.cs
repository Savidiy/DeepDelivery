using ProgressModule;

namespace MainModule
{
    public sealed class LevelRestarter
    {
        private readonly PlayerHolder _playerHolder;
        private readonly LevelHolder _levelHolder;
        private readonly ProgressProvider _progressProvider;
        private readonly BulletHolder _bulletHolder;

        public LevelRestarter(PlayerHolder playerHolder, LevelHolder levelHolder, ProgressProvider progressProvider,
            BulletHolder bulletHolder)
        {
            _playerHolder = playerHolder;
            _levelHolder = levelHolder;
            _progressProvider = progressProvider;
            _bulletHolder = bulletHolder;
        }

        public void RestartLevel()
        {
            _progressProvider.ResetProgress();
            _levelHolder.LoadCurrentLevel();
            _playerHolder.CreatePlayer();
            _bulletHolder.ClearBullets();
        }
    }
}