using System.Collections.Generic;
using MvvmModule;
using SettingsModule;
using UnityEngine;

namespace MainModule
{
    public sealed class LevelModel : DisposableCollector
    {
        private readonly LevelBehaviour _levelBehaviour;
        private readonly List<Enemy> _enemies;

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public LevelModel(LevelBehaviour levelBehaviour, List<Enemy> enemies)
        {
            _levelBehaviour = levelBehaviour;
            _enemies = enemies;
        }

        public Vector3 GetPlayerStartPosition()
        {
            return _levelBehaviour.StartPoint.position;
        }

        public override void Dispose()
        {
            base.Dispose();
            Object.Destroy(_levelBehaviour.gameObject);
            foreach (Enemy enemy in _enemies)
                enemy.Dispose();

            _enemies.Clear();
        }
    }
}