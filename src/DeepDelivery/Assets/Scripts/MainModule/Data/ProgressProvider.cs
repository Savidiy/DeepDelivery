using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class ProgressProvider
    {
        private const string PROGRESS_KEY = "PROGRESS_LEVEL";

        private readonly GameStaticData _gameStaticData;
        private readonly Serializer<Progress> _serializer = new();

        public Progress Progress { get; private set; }

        public ProgressProvider(GameStaticData gameStaticData)
        {
            _gameStaticData = gameStaticData;

            Progress = HaveSavedProgress()
                ? LoadProgress()
                : CreateDefaultProgress();
        }

        private bool HaveSavedProgress()
        {
            return PlayerPrefs.HasKey(PROGRESS_KEY);
        }

        private Progress LoadProgress()
        {
            string text = PlayerPrefs.GetString(PROGRESS_KEY);
            return _serializer.Deserialize(text);
        }

        public void ResetProgress()
        {
            Progress = CreateDefaultProgress();
            Debug.Log($"Reset progress");
        }

        private Progress CreateDefaultProgress()
        {
            var progress = new Progress(_gameStaticData.StartProgress);
            return progress;
        }

        private void SaveProgress(Progress progress)
        {
            string text = _serializer.Serialize(progress);
            PlayerPrefs.SetString(PROGRESS_KEY, text);
            PlayerPrefs.Save();
        }
    }
}