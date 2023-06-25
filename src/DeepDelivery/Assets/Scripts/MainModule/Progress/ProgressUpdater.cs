using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    public class ProgressUpdater : IDisposable
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

        public void Unregister(IProgressReader reader)
        {
            _progressReaders.Remove(reader);
            if (reader is IProgressWriter writer)
                _progressWriters.Remove(writer);
        }

        public void ResetProgress()
        {
            Progress progress = _progressSaveLoad.CreateDefaultProgress();
            _progressProvider.Progress = progress;
            _progressSaveLoad.SaveProgress(progress);
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

        public void Dispose()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }
    }
}