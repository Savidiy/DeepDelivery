using LevelWindowModule.Contracts;

namespace MainModule
{
    public sealed class LevelRestarter
    {
        private readonly PlayerHolder _playerHolder;
        private readonly ProgressUpdater _progressUpdater;
        private readonly BulletHolder _bulletHolder;
        private readonly PlayerInvulnerability _playerInvulnerability;
        private readonly ILevelWindowPresenter _levelWindowPresenter;

        public LevelRestarter(PlayerHolder playerHolder, ProgressUpdater progressUpdater,
            BulletHolder bulletHolder, PlayerInvulnerability playerInvulnerability, ILevelWindowPresenter levelWindowPresenter)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _playerHolder = playerHolder;
            _progressUpdater = progressUpdater;
            _bulletHolder = bulletHolder;
            _playerInvulnerability = playerInvulnerability;
        }

        public void RestartLevel()
        {
            _progressUpdater.ResetProgress();
            LoadLevel();
        }

        public void LoadLevel()
        {
            _progressUpdater.PublishProgress();
            
            _playerHolder.CreatePlayer();
            _bulletHolder.ClearBullets();
            _playerInvulnerability.StartInvulnerableTimer();
            _levelWindowPresenter.HideWindow();
            _levelWindowPresenter.ShowWindow();
        }
    }
}