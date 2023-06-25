using DG.Tweening;
using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class DeathPauseLevelState : IState, IStateWithExit, ILevelState
    {
        private readonly GameStaticData _gameStaticData;
        private readonly LevelStateMachine _levelStateMachine;

        private Sequence _restartTween;

        public DeathPauseLevelState(GameStaticData gameStaticData, LevelStateMachine levelStateMachine)
        {
            _gameStaticData = gameStaticData;
            _levelStateMachine = levelStateMachine;
        }

        public void Enter()
        {
            _restartTween = DOTween.Sequence()
                .AppendInterval(_gameStaticData.HitInvulDuration)
                .AppendCallback(EndDelay);
        }

        private void EndDelay()
        {
            _restartTween = null;
            var args = new ClearLevelStateArgs(true);
            _levelStateMachine.EnterToState<ClearLevelState, ClearLevelStateArgs>(args);
        }

        public void Exit()
        {
            _restartTween?.Kill();
            _restartTween = null;
        }
    }
}