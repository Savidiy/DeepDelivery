using Zenject;

namespace MainModule
{
    public class CheckPointFactory : IFactory<CheckPoint, CheckPointBehaviour>
    {
        private readonly DiContainer _diContainer;

        public CheckPointFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        public CheckPoint Create(CheckPointBehaviour data)
        {
            var progressUpdater = _diContainer.Resolve<ProgressUpdater>();
            return new CheckPoint(data, progressUpdater);
        }
    }
}