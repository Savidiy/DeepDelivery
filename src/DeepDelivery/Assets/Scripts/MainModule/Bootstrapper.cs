using UnityEngine;
using Zenject;

namespace MainModule
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        private MainStateMachine _mainStateMachine;

        [Inject]
        public void Construct(MainStateMachine mainStateMachine, StartMainState startMainState,
            LevelPlayMainState levelPlayMainState, LevelStateMachine levelStateMachine, PlayLevelState playLevelState,
            LoadLevelState loadLevelState, ResetProgressLevelState resetProgressLevelState,
            DeathPauseLevelState deathPauseLevelState, ClearLevelState clearLevelState)
        {
            _mainStateMachine = mainStateMachine;

            _mainStateMachine.AddState(startMainState);
            _mainStateMachine.AddState(levelPlayMainState);

            levelStateMachine.AddState(loadLevelState);
            levelStateMachine.AddState(resetProgressLevelState);
            levelStateMachine.AddState(playLevelState);
            levelStateMachine.AddState(deathPauseLevelState);
            levelStateMachine.AddState<ClearLevelState, ClearLevelStateArgs>(clearLevelState);
        }

        public void Awake()
        {
            _mainStateMachine.EnterToState<StartMainState>();
        }
    }
}