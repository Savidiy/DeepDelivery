using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    public class ProgressUpdater
    {
        private readonly List<IProgressReader> _progressReaders = new();
        private readonly List<IProgressWriter> _progressWriters = new();
        private readonly ProgressSaveLoad _progressSaveLoad;
        private readonly ProgressProvider _progressProvider;

        public ProgressUpdater(List<IProgressWriter> progressWriters, List<IProgressReader> progressReaders,
            ProgressSaveLoad progressSaveLoad, ProgressProvider progressProvider)
        {
            _progressSaveLoad = progressSaveLoad;
            _progressProvider = progressProvider;
            _progressReaders.AddRange(progressReaders);
            _progressWriters.AddRange(progressWriters);
        }

        public void ResetProgress()
        {
            _progressProvider.Progress = _progressSaveLoad.CreateDefaultProgress();
            Debug.Log($"Reset progress");
        }

        public void PublishProgress()
        {
            for (var index = 0; index < _progressReaders.Count; index++)
            {
                IProgressReader progressReader = _progressReaders[index];
                progressReader.LoadProgress(_progressProvider.Progress);
            }
        }

        public void SaveProgress()
        {
            foreach (IProgressWriter progressWriter in _progressWriters)
                progressWriter.UpdateProgress(_progressProvider.Progress);

            _progressSaveLoad.SaveProgress(_progressProvider.Progress);
        }

        public void AddProgressWriter(IProgressWriter progressWriter)
        {
            _progressWriters.Add(progressWriter);
            _progressReaders.Add(progressWriter);
        }

        public void RemoveProgressWriter(IProgressWriter progressWriter)
        {
            _progressWriters.Remove(progressWriter);
            _progressReaders.Remove(progressWriter);
        }

        public void AddProgressReader(IProgressReader progressReader) => _progressReaders.Add(progressReader);
        public void RemoveProgressReader(IProgressReader progressReader) => _progressReaders.Remove(progressReader);
    }
}