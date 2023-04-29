using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class LevelModelFactory
    {
        public LevelModel Create(LevelData levelData)
        {
            LevelBehaviour levelBehaviour = Object.Instantiate(levelData.LevelBehaviour);
            var levelModel = new LevelModel(levelBehaviour);
            return levelModel;
        }
    }
}