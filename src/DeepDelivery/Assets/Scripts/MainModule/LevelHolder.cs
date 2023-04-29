using System;
using ProgressModule;
using Savidiy.Utils;
using SettingsModule;

namespace MainModule
{
    public sealed class LevelHolder : IDisposable
    {
        private readonly GameStaticData _gameStaticData;
        private readonly ProgressProvider _progressProvider;
        private readonly LevelModelFactory _levelModelFactory;
        private readonly TickInvoker _tickInvoker;

        public LevelModel LevelModel { get; private set; }

        public LevelHolder(GameStaticData gameStaticData, ProgressProvider progressProvider, LevelModelFactory levelModelFactory,
            TickInvoker tickInvoker)
        {
            _gameStaticData = gameStaticData;
            _progressProvider = progressProvider;
            _levelModelFactory = levelModelFactory;
            _tickInvoker = tickInvoker;
            
            _tickInvoker.Updated += OnUpdated;
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

        private void OnUpdated()
        {
            for (int index = LevelModel.Enemies.Count - 1; index >= 0; index--)
            {
                Enemy enemy = LevelModel.Enemies[index];
                if (enemy.Hp <= 0)
                    LevelModel.RemoveEnemyAt(index);
            }
        }
    }
}