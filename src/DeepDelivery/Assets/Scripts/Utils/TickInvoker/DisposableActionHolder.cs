using System;

namespace Savidiy.Utils
{
    public sealed class DisposableActionHolder : IDisposable
    {
        private readonly Action _disposeAction;
        public DisposableActionHolder(Action disposeAction)
        {
            _disposeAction = disposeAction;
        }
        public void Dispose()
        {
            _disposeAction?.Invoke();
        }
    }
}