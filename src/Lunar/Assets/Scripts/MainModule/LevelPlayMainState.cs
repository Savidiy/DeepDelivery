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

        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter, LevelHolder levelHolder, PlayerHolder playerHolder,
            PlayerInputMover playerInputMover)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _levelHolder = levelHolder;
            _playerHolder = playerHolder;
            _playerInputMover = playerInputMover;
        }

        public void Enter()
        {
            _levelHolder.LoadCurrentLevel();
            _levelWindowPresenter.ShowWindow();
            _playerHolder.CreatePlayer();
            _playerInputMover.ActivatePlayerControls();
        }

        public void Exit()
        {
            _levelWindowPresenter.HideWindow();
            _playerInputMover.DeactivatePlayerControls();
        }
    }
}