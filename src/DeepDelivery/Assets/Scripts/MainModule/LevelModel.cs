using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
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

        public bool HasCollisionWithWalls(Collider2D collider)
        {
            foreach (Collider2D wall in _levelBehaviour.Walls)
            {
                if (wall.HasCollisionWith(collider))
                {
                    return true;
                }
            }

            return false;
        }

        public void RemoveEnemyAt(int index)
        {
            _enemies[index].Dispose();
            _enemies.RemoveAt(index);
        }
    }
}