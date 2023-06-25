using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class LevelPlayMainState : IState, IStateWithExit, IMainState
    {
        private readonly LevelStateMachine _levelStateMachine;

        public LevelPlayMainState(LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
        }

        public void Enter()
        {
            _levelStateMachine.EnterToState<LoadLevelState>();
        }

        public void Exit()
        {
            var args = new ClearLevelStateArgs(needLoadAfterClear: false);
            _levelStateMachine.EnterToState<ClearLevelState, ClearLevelStateArgs>(args);
        }
    }
}