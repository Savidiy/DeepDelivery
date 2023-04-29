using System.Collections.Generic;
using MvvmModule;
using SettingsModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class LevelModel : DisposableCollector
    {
        private readonly LevelBehaviour _levelBehaviour;
        private readonly List<Enemy> _enemies;

        public LevelModel(LevelBehaviour levelBehaviour, List<Enemy> enemies)
        {
            _levelBehaviour = levelBehaviour;
            _enemies = enemies;
        }

        public Vector3 GetPlayerStartPosition()
        {
            return _levelBehaviour.StartPoint.position;
        }
    }
}