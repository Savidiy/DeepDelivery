using JetBrains.Annotations;
using Savidiy.Utils;

namespace MainModule
{
    public class UseCheckPointChecker : IProgressWriter
    {
        private readonly LevelHolder _levelHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        [CanBeNull] private CheckPoint _previousUsedCheckPoint;

        public UseCheckPointChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder, ProgressUpdater progressUpdater)
        {
            _levelHolder = levelHolder;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            progressUpdater.Register(this);
        }

        public void LoadProgress(Progress progress)
        {
            foreach (CheckPoint checkPoint in _levelHolder.LevelModel.CheckPoints)
            {
                if (checkPoint.Id.Equals(progress.LastActiveCheckPointId))
                {
                    _previousUsedCheckPoint = checkPoint;
                    return;
                }
            }

            _previousUsedCheckPoint = null;
        }

        public void UpdateProgress(Progress progress)
        {
            progress.LastActiveCheckPointId = _previousUsedCheckPoint == null
                ? string.Empty
                : _previousUsedCheckPoint.Id;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            Player player = _playerHolder.Player;

            foreach (CheckPoint checkPoint in _levelHolder.LevelModel.CheckPoints)
            {
                if (!checkPoint.CanUseCheckPoint(player))
                    continue;

                UseCheckPoint(checkPoint);
                return;
            }
        }

        private void UseCheckPoint(CheckPoint checkPoint)
        {
            _previousUsedCheckPoint?.SetUnused();
            _previousUsedCheckPoint = checkPoint;
            checkPoint.UseCheckPoint();
        }
    }
}