using System;
using UnityEngine;
using Zenject;

namespace Savidiy.Utils
{
    public class TickInvoker : ITickable, ILateTickable, IFixedTickable
    {
        private bool _isPause;
        public float DeltaTime { get; private set; }
        public float FixedDeltaTime { get; private set; }
        
        private readonly EventActionList _onUpdateActions = new();
        private readonly EventActionList _onLateUpdateActions = new();
        private readonly EventActionList _onFixedUpdateActions = new();

        public void Tick()
        {
            if (_isPause)
                return;

            DeltaTime = Time.deltaTime;
            _onUpdateActions.InvokeActions();
        }

        public void LateTick()
        {
            if (_isPause)
                return;

            _onLateUpdateActions.InvokeActions();
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
            _onFixedUpdateActions.InvokeActions();
        }

        public IDisposable Subscribe(UpdateType eventType, Action action)
        {
            switch (eventType)
            {
                case UpdateType.Update:
                    _onUpdateActions.Add(action);
                    break;
                case UpdateType.FixedUpdate:
                    _onFixedUpdateActions.Add(action);
                    break;
                case UpdateType.LateUpdate:
                    _onLateUpdateActions.Add(action);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventType), eventType, null);
            }

            return new DisposableActionHolder(() =>
            {
                Unsubscribe(eventType, action);
            });
        }

        public void Unsubscribe(UpdateType eventTypeType, Action action)
        {
            switch (eventTypeType)
            {
                case UpdateType.Update:
                    _onUpdateActions.Remove(action);
                    break;
                case UpdateType.FixedUpdate:
                    _onFixedUpdateActions.Remove(action);
                    break;
                case UpdateType.LateUpdate:
                    _onLateUpdateActions.Remove(action);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(eventTypeType), eventTypeType, null);
            }
        }
    }
}