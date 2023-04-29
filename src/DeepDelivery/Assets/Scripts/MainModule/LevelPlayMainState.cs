using LevelWindowModule;
using LevelWindowModule.Contracts;
using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class LevelPlayMainState : IState, IStateWithExit, IMainState
    {
        private readonly ILevelWindowPresenter _levelWindowPresenter;
        private readonly PlayerInputMover _playerInputMover;
        private readonly CameraToPlayerMover _cameraToPlayerMover;
        private readonly LevelRestarter _levelRestarter;

        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter,
            PlayerInputMover playerInputMover, CameraToPlayerMover cameraToPlayerMover, LevelRestarter levelRestarter)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _playerInputMover = playerInputMover;
            _cameraToPlayerMover = cameraToPlayerMover;
            _levelRestarter = levelRestarter;
        }

        public void Enter()
        {
            _levelRestarter.RestartLevel();
            _playerInputMover.ActivatePlayerControls();
            _cameraToPlayerMover.Activate();
            _levelWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _levelWindowPresenter.HideWindow();
            _playerInputMover.DeactivatePlayerControls();
            _cameraToPlayerMover.Deactivate();
        }
    }
}