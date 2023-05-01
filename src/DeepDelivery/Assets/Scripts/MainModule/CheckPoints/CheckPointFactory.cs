namespace MainModule
{
    public class CheckPointFactory : IFactory<CheckPoint, CheckPointBehaviour>
    {
        private readonly ProgressUpdater _progressUpdater;

        public CheckPointFactory(ProgressUpdater progressUpdater)
        {
            _progressUpdater = progressUpdater;
        }
        
        public CheckPoint Create(CheckPointBehaviour data)
        {
            return new CheckPoint(data, _progressUpdater);
        }
    }
}