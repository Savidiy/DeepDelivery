using Savidiy.Utils;

namespace MainModule
{
    public class CheckPoint : IProgressReader
    {
        private readonly CheckPointBehaviour _behaviour;
        private readonly ProgressUpdater _progressUpdater;
        private bool _isActivated;

        public string Id => _behaviour.UniqueId.Id;

        public CheckPoint(CheckPointBehaviour behaviour, ProgressUpdater progressUpdater)
        {
            _behaviour = behaviour;
            _progressUpdater = progressUpdater;
        }

        public void LoadProgress(Progress progress)
        {
            if (_behaviour.UniqueId.Id.Equals(progress.LastActiveCheckPointId))
                SetUsed();
            else
                SetUnused();
        }

        public bool CanUseCheckPoint(Player player)
        {
            if (_isActivated)
                return false;
            
            return player.Collider.HasCollisionWith(_behaviour.Collider);
        }

        public void SetUnused()
        {
            _isActivated = false;
            _behaviour.MarkUnused();
        }

        public void UseCheckPoint()
        {
            SetUsed();
            _progressUpdater.SaveProgress();
        }

        private void SetUsed()
        {
            _isActivated = true;
            _behaviour.MarkUsed();
        }
    }
}