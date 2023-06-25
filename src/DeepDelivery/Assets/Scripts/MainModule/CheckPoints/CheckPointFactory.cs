using System.Collections.Generic;

namespace MainModule
{
    public class CheckPointFactory
    {
        private readonly ProgressUpdater _progressUpdater;

        public CheckPointFactory(ProgressUpdater progressUpdater)
        {
            _progressUpdater = progressUpdater;
        }

        public List<CheckPoint> CreatePoints(List<CheckPointBehaviour> checkPointBehaviours)
        {
            var checkPoints = new List<CheckPoint>();
            foreach (CheckPointBehaviour behaviour in checkPointBehaviours)
                checkPoints.Add(Create(behaviour));

            return checkPoints;
        }

        private CheckPoint Create(CheckPointBehaviour data)
        {
            return new CheckPoint(data, _progressUpdater);
        }
    }
}