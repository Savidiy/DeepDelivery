using LevelWindowModule.Contracts;

namespace MainModule
{
    public sealed class LevelRestarter
    {
        private readonly ProgressUpdater _progressUpdater;
        private readonly CollisionWithEnemyChecker _collisionWithEnemyChecker;
        private readonly BulletHolder _bulletHolder;
        private readonly PlayerInvulnerability _playerInvulnerability;
        private readonly ILevelWindowPresenter _levelWindowPresenter;

        public LevelRestarter(ProgressUpdater progressUpdater, CollisionWithEnemyChecker collisionWithEnemyChecker,
            BulletHolder bulletHolder, PlayerInvulnerability playerInvulnerability, ILevelWindowPresenter levelWindowPresenter)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _progressUpdater = progressUpdater;
            _collisionWithEnemyChecker = collisionWithEnemyChecker;
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
            _playerInvulnerability.StartInvulnerableTimer();

            _collisionWithEnemyChecker.ClearCollisions();
            _bulletHolder.ClearBullets();
            _levelWindowPresenter.HideWindow();
            _levelWindowPresenter.ShowWindow();
        }
    }
}