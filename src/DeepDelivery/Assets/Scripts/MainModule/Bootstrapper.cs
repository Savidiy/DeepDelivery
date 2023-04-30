using Zenject;

namespace MainModule
{
    public sealed class Bootstrapper : IInitializable
    {
        private readonly MainStateMachine _mainStateMachine;

        public Bootstrapper(MainStateMachine mainStateMachine, StartMainState startMainState,
            LevelPlayMainState levelPlayMainState)
        {
            _mainStateMachine = mainStateMachine;

            _mainStateMachine.AddState(startMainState);
            _mainStateMachine.AddState(levelPlayMainState);
        }

        public void Initialize()
        {
            _mainStateMachine.EnterToState<LevelPlayMainState>();
        }
    }
}