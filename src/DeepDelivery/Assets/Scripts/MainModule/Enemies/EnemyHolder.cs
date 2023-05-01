using System;
using System.Collections.Generic;

namespace MainModule
{
    public class EnemyHolder : IDisposable
    {
        private readonly List<Enemy> _enemies = new();

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(Enemy enemy)
        {
            _enemies.Remove(enemy);
        }

        public void Clear()
        {
            _enemies.Clear();
        }

        public void Dispose()
        {
        }
    }
}