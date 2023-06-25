using System;

namespace MainModule
{
    public sealed class LevelHolder : IDisposable
    {
        public LevelModel LevelModel { get; private set; }

        public void AddLevel(LevelModel levelModel)
        {
            LevelModel = levelModel;
        }

        public void RemoveLevel()
        {
            LevelModel.Dispose();
            LevelModel = null;
        }

        public void Dispose()
        {
            LevelModel?.Dispose();
        }
    }
}