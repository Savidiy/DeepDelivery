using LevelWindowModule;
using LevelWindowModule.Contracts;
using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class LevelPlayMainState :  IState, IStateWithExit, IMainState
    {
        private readonly ILevelWindowPresenter _levelWindowPresenter;
        private readonly LevelHolder _levelHolder;

        public LevelPlayMainState(ILevelWindowPresenter levelWindowPresenter, LevelHolder levelHolder)
        {
            _levelWindowPresenter = levelWindowPresenter;
            _levelHolder = levelHolder;
        }
        
        public void Enter()
        {
            _levelHolder.LoadCurrentLevel();
            _levelWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _levelWindowPresenter.HideWindow();
        }
    }
}