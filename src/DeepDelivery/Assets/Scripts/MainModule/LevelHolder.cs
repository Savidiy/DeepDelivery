using Progress;
using SettingsModule;

namespace MainModule
{
    public sealed class LevelHolder
    {
        private readonly GameSettings _gameSettings;
        private readonly ProgressProvider _progressProvider;
        private readonly LevelModelFactory _levelModelFactory;

        public LevelModel LevelModel { get; private set; }

        public LevelHolder(GameSettings gameSettings, ProgressProvider progressProvider, LevelModelFactory levelModelFactory)
        {
            _gameSettings = gameSettings;
            _progressProvider = progressProvider;
            _levelModelFactory = levelModelFactory;
        }

        public void LoadCurrentLevel()
        {
            LevelModel?.Dispose();

            int currentLevel = _progressProvider.CurrentLevel;
            LevelData levelData = _gameSettings.Levels[currentLevel];
            LevelModel = _levelModelFactory.Create(levelData);
        }
    }
}