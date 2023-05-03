using UnityEngine;
using Zenject;

namespace MainModule
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        private MainStateMachine _mainStateMachine;

        [Inject]
        public void Construct(MainStateMachine mainStateMachine, StartMainState startMainState,
            LevelPlayMainState levelPlayMainState)
        {
            _mainStateMachine = mainStateMachine;

            _mainStateMachine.AddState(startMainState);
            _mainStateMachine.AddState(levelPlayMainState);
        }

        public void Awake()
        {
            _mainStateMachine.EnterToState<StartMainState>();
        }
    }
}