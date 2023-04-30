using System;
using Savidiy.Utils;

namespace MainModule
{
    public class EnemyMoveUpdater : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly LevelHolder _levelHolder;

        public EnemyMoveUpdater(TickInvoker tickInvoker, LevelHolder levelHolder)
        {
            _tickInvoker = tickInvoker;
            _levelHolder = levelHolder;
            
            _tickInvoker.Updated += OnUpdated;
        }

        public void Dispose()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            float deltaTime = _tickInvoker.DeltaTime;

            foreach (Enemy enemy in _levelHolder.LevelModel.Enemies)
            {
                enemy.EnemyMover.UpdatePosition(deltaTime);
            }
        }
    }
}