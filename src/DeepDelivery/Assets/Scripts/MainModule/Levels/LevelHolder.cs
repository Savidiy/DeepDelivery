using System;

namespace MainModule
{
    public sealed class LevelHolder : IDisposable, IProgressWriter
    {
        private readonly GameStaticData _gameStaticData;
        private readonly LevelModelFactory _levelModelFactory;

        public LevelModel LevelModel { get; private set; }

        public LevelHolder(GameStaticData gameStaticData, LevelModelFactory levelModelFactory, ProgressUpdater progressUpdater)
        {
            _gameStaticData = gameStaticData;
            _levelModelFactory = levelModelFactory;
            progressUpdater.Register(this);
        }

        public void LoadProgress(Progress progress)
        {
            LevelModel?.Dispose();

            int levelIndex = progress.LevelIndex;
            LevelData levelData = _gameStaticData.Levels[levelIndex];
            LevelModel = _levelModelFactory.Create(levelData);
            LevelModel.LoadProgress(progress);
        }

        public void UpdateProgress(Progress progress)
        {
            LevelModel.UpdateProgress(progress);
        }

        public void Dispose()
        {
            LevelModel?.Dispose();
        }
    }
}