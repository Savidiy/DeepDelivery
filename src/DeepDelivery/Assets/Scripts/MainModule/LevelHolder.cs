using System;
using ProgressModule;
using SettingsModule;

namespace MainModule
{
    public sealed class LevelHolder : IDisposable
    {
        private readonly GameStaticData _gameStaticData;
        private readonly ProgressProvider _progressProvider;
        private readonly LevelModelFactory _levelModelFactory;

        public LevelModel LevelModel { get; private set; }

        public LevelHolder(GameStaticData gameStaticData, ProgressProvider progressProvider, LevelModelFactory levelModelFactory)
        {
            _gameStaticData = gameStaticData;
            _progressProvider = progressProvider;
            _levelModelFactory = levelModelFactory;
        }

        public void LoadCurrentLevel()
        {
            LevelModel?.Dispose();

            int levelIndex = _progressProvider.Progress.LevelIndex;
            LevelData levelData = _gameStaticData.Levels[levelIndex];
            LevelModel = _levelModelFactory.Create(levelData);
        }

        public void Dispose()
        {
            LevelModel?.Dispose();
        }
    }
}