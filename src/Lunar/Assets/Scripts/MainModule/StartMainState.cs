using Savidiy.Utils.StateMachine;
using StartWindowModule.Contracts;

namespace MainModule
{
    public sealed class StartMainState : IState, IStateWithExit, IMainState
    {
        private readonly IStartWindowPresenter _startWindowPresenter;

        public StartMainState(IStartWindowPresenter startWindowPresenter)
        {
            _startWindowPresenter = startWindowPresenter;
        }
        
        public void Enter()
        {
            _startWindowPresenter.ShowWindow();
        }

        public void Exit()
        {
            _startWindowPresenter.HideWindow();
        }
    }
}