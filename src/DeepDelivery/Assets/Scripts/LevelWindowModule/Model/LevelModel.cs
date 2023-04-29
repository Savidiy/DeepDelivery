using MvvmModule;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class LevelModel : DisposableCollector
    {
        private readonly LevelBehaviour _levelBehaviour;

        public LevelModel(LevelBehaviour levelBehaviour)
        {
            _levelBehaviour = levelBehaviour;
        }

        public Vector3 GetPlayerStartPosition()
        {
            return _levelBehaviour.StartPoint.position;
        }
    }
}