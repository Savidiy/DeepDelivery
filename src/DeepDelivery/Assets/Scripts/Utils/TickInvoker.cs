using System;
using UnityEngine;
using Zenject;

namespace Savidiy.Utils
{
    public class TickInvoker : ITickable, ILateTickable, IFixedTickable
    {
        private bool _isPause;
        public event Action Updated;
        public event Action LateUpdated;
        public event Action FixedUpdated;
        public float DeltaTime { get; private set; }
        public float FixedDeltaTime { get; private set; }

        public void Tick()
        {
            if (_isPause)
                return;

            DeltaTime = Time.deltaTime;
            Updated?.Invoke();
        }

        public void LateTick()
        {
            if (_isPause)
                return;

            LateUpdated?.Invoke();
        }

        public void SetPause(bool isPause)
        {
            _isPause = isPause;
        }

        public void FixedTick()
        {
            if (_isPause)
                return;

            FixedDeltaTime = Time.fixedDeltaTime;
            FixedUpdated?.Invoke();
        }
    }
}