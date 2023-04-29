using LevelWindowModule;
using LevelWindowModule.Contracts;
using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class LevelPlayMainState : IState, IStateWithExit, IMainState
    {
        private readonly ILevelWindowPresenter _levelWindowPresenter;
        private readonly LevelHolder _levelHolder;
        private readonly PlayerHolder _playerHolder;
        private readonly PlayerInputMover _playerInputMover;
        private readonly CameraToPlayerMover _cameraToPlayerMover;
     
        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter, LevelHolder levelHolder, PlayerHolder playerHolder,
            PlayerInputMover playerInputMover, CameraToPlayerMover cameraToPlayerMover)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _levelHolder = levelHolder;
            _playerHolder = playerHolder;
            _playerInputMover = playerInputMover;
            _cameraToPlayerMover = cameraToPlayerMover;
        }

        public void Enter()
        {
            _levelHolder.LoadCurrentLevel();
            _levelWindowPresenter.ShowWindow();
            _playerHolder.CreatePlayer();
            _playerInputMover.ActivatePlayerControls();
            _cameraToPlayerMover.Activate();
        }

        public void Exit()
        {
            _levelWindowPresenter.HideWindow();
            _playerInputMover.DeactivatePlayerControls();
            _cameraToPlayerMover.Deactivate();
        }
    }
}