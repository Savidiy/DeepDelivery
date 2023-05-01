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

        public ProgressUpdater(ProgressSaveLoad progressSaveLoad, ProgressProvider progressProvider)
        {
            _progressSaveLoad = progressSaveLoad;
            _progressProvider = progressProvider;
        }

        public void Register(IProgressReader reader)
        {
            _progressReaders.Add(reader);
            if (reader is IProgressWriter writer)
                _progressWriters.Add(writer);
        }

        public void ResetProgress()
        {
            _progressProvider.Progress = _progressSaveLoad.CreateDefaultProgress();
            Debug.Log($"Reset progress");
        }

        public void PublishProgress()
        {
            foreach (IProgressReader progressReader in _progressReaders)
                progressReader.LoadProgress(_progressProvider.Progress);
        }

        public void SaveProgress()
        {
            foreach (IProgressWriter progressWriter in _progressWriters)
                progressWriter.UpdateProgress(_progressProvider.Progress);

            _progressSaveLoad.SaveProgress(_progressProvider.Progress);
        }
    }
}