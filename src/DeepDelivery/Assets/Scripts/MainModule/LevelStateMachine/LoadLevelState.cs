using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public sealed class LoadLevelState : IState, ILevelState
    {
        private readonly ProgressUpdater _progressUpdater;
        private readonly PlayerInvulnerability _playerInvulnerability;
        private readonly LevelStateMachine _levelStateMachine;
        private readonly PlayerHolder _playerHolder;
        private readonly PlayerFactory _playerFactory;
        private readonly LevelHolder _levelHolder;
        private readonly GameStaticData _gameStaticData;
        private readonly LevelModelFactory _levelModelFactory;
        private readonly ProgressProvider _progressProvider;

        public LoadLevelState(ProgressUpdater progressUpdater, PlayerInvulnerability playerInvulnerability,
            LevelStateMachine levelStateMachine, PlayerHolder playerHolder, PlayerFactory playerFactory, LevelHolder levelHolder,
            GameStaticData gameStaticData, LevelModelFactory levelModelFactory, ProgressProvider progressProvider)
        {
            _levelStateMachine = levelStateMachine;
            _playerHolder = playerHolder;
            _playerFactory = playerFactory;
            _levelHolder = levelHolder;
            _gameStaticData = gameStaticData;
            _levelModelFactory = levelModelFactory;
            _progressProvider = progressProvider;
            _progressUpdater = progressUpdater;
            _playerInvulnerability = playerInvulnerability;
        }

        public void Enter()
        {
            int levelIndex = _progressProvider.Progress.LevelIndex;
            LevelData levelData = _gameStaticData.Levels[levelIndex];
            LevelModel levelModel = _levelModelFactory.Create(levelData);
            _levelHolder.AddLevel(levelModel);
            
            PlayerVisual playerVisual = _playerFactory.CreatePlayer();
            _playerHolder.AddPlayer(playerVisual);
            
            _playerInvulnerability.StartInvulnerableTimer();
            _progressUpdater.PublishProgress();

            _levelStateMachine.EnterToState<PlayLevelState>();
        }
    }
}