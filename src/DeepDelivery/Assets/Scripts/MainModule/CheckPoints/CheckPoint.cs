using Savidiy.Utils;

namespace MainModule
{
    public class CheckPoint : IProgressReader
    {
        private readonly CheckPointBehaviour _data;
        private readonly ProgressUpdater _progressUpdater;
        private bool _isActivated;

        public string Id => _data.UniqueId.Id;

        public CheckPoint(CheckPointBehaviour data, ProgressUpdater progressUpdater)
        {
            _data = data;
            _progressUpdater = progressUpdater;
        }

        public void LoadProgress(Progress progress)
        {
            if (_data.UniqueId.Id.Equals(progress.LastActiveCheckPointId))
                SetUsed();
            else
                SetUnused();
        }

        public bool CanUseCheckPoint(Player player)
        {
            if (_isActivated)
                return false;
            
            return player.Collider.HasCollisionWith(_data.Collider);
        }

        public void SetUnused()
        {
            _isActivated = false;
            _data.MarkUnused();
        }

        public void UseCheckPoint()
        {
            SetUsed();
            _progressUpdater.SaveProgress();
        }

        private void SetUsed()
        {
            _isActivated = true;
            _data.MarkUsed();
        }
    }
}