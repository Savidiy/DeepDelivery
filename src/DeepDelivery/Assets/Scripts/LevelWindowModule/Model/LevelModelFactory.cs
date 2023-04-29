using Savidiy.Utils;
using SettingsModule;

namespace LevelWindowModule
{
    public sealed class LevelModelFactory
    {
        private readonly TickInvoker _tickInvoker;

        public LevelModelFactory(TickInvoker tickInvoker)
        {
            _tickInvoker = tickInvoker;
        }

        public LevelModel Create(LevelData levelData)
        {
            var levelModel = new LevelModel();
            return levelModel;
        }
    }
}