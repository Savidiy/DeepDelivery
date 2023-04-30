using LevelWindowModule.Contracts;
using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class LevelPlayMainState : IState, IStateWithExit, IMainState
    {
        private readonly ILevelWindowPresenter _levelWindowPresenter;
        private readonly CollisionWithEnemyChecker _collisionWithEnemyChecker;
        private readonly PlayerInputMover _playerInputMover;
        private readonly PlayerInputShooter _playerInputShooter;
        private readonly CameraToPlayerMover _cameraToPlayerMover;
        private readonly LevelRestarter _levelRestarter;
        private readonly CollisionWithItemsChecker _collisionWithItemsChecker;

        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter, CollisionWithEnemyChecker collisionWithEnemyChecker,
            PlayerInputMover playerInputMover, PlayerInputShooter playerInputShooter, CameraToPlayerMover cameraToPlayerMover,
            LevelRestarter levelRestarter, CollisionWithItemsChecker collisionWithItemsChecker)
        {
            _collisionWithItemsChecker = collisionWithItemsChecker;
            _levelWindowPresenter = levelWindowPresenter;
            _collisionWithEnemyChecker = collisionWithEnemyChecker;
            _playerInputMover = playerInputMover;
            _playerInputShooter = playerInputShooter;
            _cameraToPlayerMover = cameraToPlayerMover;
            _levelRestarter = levelRestarter;
        }

        public void Enter()
        {
            _levelRestarter.RestartLevel();
            _playerInputMover.ActivatePlayerControls();
            _playerInputShooter.ActivatePlayerControls();
            _collisionWithEnemyChecker.Activate();
            _collisionWithItemsChecker.Activate();
            _cameraToPlayerMover.Activate();
            _levelWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _levelWindowPresenter.HideWindow();
            _playerInputMover.DeactivatePlayerControls();
            _playerInputShooter.DeactivatePlayerControls();
            _cameraToPlayerMover.Deactivate();
            _collisionWithEnemyChecker.Deactivate();
            _collisionWithItemsChecker.Deactivate();
        }
    }
}