using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class ResetProgressLevelState : IState, ILevelState
    {
        private readonly ProgressUpdater _progressUpdater;
        private readonly LevelStateMachine _levelStateMachine;

        public ResetProgressLevelState(ProgressUpdater progressUpdater, LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
            _progressUpdater = progressUpdater;
        }

        public void Enter()
        {
            _progressUpdater.ResetProgress();
            var args = new ClearLevelStateArgs(true);
            _levelStateMachine.EnterToState<ClearLevelState, ClearLevelStateArgs>(args);    }
    }
}