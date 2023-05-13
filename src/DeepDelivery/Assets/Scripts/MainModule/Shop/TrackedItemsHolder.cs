using System.Collections.Generic;
using AudioModule.Contracts;

namespace MainModule
{
    public class TrackedItemsHolder : IProgressWriter
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly List<ItemType> _trackedItems = new();

        public IReadOnlyList<ItemType> TrackedItems => _trackedItems;

        public TrackedItemsHolder(IAudioPlayer audioPlayer, ProgressUpdater progressUpdater)
        {
            _audioPlayer = audioPlayer;
            progressUpdater.Register(this);
        }

        public void TrackItem(ItemType itemType)
        {
            if (_trackedItems.Contains(itemType))
                return;

            _trackedItems.Add(itemType);
            _audioPlayer.PlayOnce(SoundId.Sonar);
        }

        public void UntrackItem(ItemType itemType)
        {
            if (_trackedItems.Contains(itemType))
                _trackedItems.Remove(itemType);
        }

        public void LoadProgress(Progress progress)
        {
            _trackedItems.Clear();
            if (progress.TrackedItems != null)
                _trackedItems.AddRange(progress.TrackedItems);
        }

        public void UpdateProgress(Progress progress)
        {
            progress.TrackedItems = new();
            progress.TrackedItems.AddRange(_trackedItems);
        }
    }
}