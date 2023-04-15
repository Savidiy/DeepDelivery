using System;
using UnityEngine;
using Zenject;

namespace Savidiy.Utils
{
    public class TickInvoker : ITickable, ILateTickable
    {
        private bool _isPause;
        public event Action Updated;
        public event Action LateUpdated;
        public float DeltaTime { get; private set; }

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
    }
}