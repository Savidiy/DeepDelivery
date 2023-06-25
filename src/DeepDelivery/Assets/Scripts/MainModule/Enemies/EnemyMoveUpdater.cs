using System;
using Savidiy.Utils;

namespace MainModule
{
    public class EnemyMoveUpdater : IDisposable
    {
        private readonly TickInvoker _tickInvoker;
        private readonly EnemyHolder _enemyHolder;

        public EnemyMoveUpdater(TickInvoker tickInvoker, EnemyHolder enemyHolder)
        {
            _tickInvoker = tickInvoker;
            _enemyHolder = enemyHolder;
            
            _tickInvoker.Subscribe(UpdateType.Update, OnUpdated);
        }

        public void Dispose()
        {
            _tickInvoker.Unsubscribe(UpdateType.Update, OnUpdated);
        }

        private void OnUpdated()
        {
            float deltaTime = _tickInvoker.DeltaTime;

            foreach (Enemy enemy in _enemyHolder.Enemies)
            {
                enemy.Move(deltaTime);
            }
        }
    }
}