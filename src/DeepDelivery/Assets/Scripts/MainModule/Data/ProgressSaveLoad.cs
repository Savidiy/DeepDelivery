using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class ProgressSaveLoad
    {
        private const string PROGRESS_KEY = "PROGRESS_LEVEL";

        private readonly GameStaticData _gameStaticData;
        private readonly Serializer<Progress> _serializer = new();

        public ProgressSaveLoad(GameStaticData gameStaticData)
        {
            _gameStaticData = gameStaticData;
        }

        public Progress CreateDefaultProgress()
        {
            var progress = new Progress(_gameStaticData.StartProgress);
            return progress;
        }

        public Progress LoadProgress()
        {
            if (HaveSavedProgress())
            {
                string text = PlayerPrefs.GetString(PROGRESS_KEY);
                return _serializer.Deserialize(text);
            }

            return CreateDefaultProgress();
        }

        public void SaveProgress(Progress progress)
        {
            string text = _serializer.Serialize(progress);
            PlayerPrefs.SetString(PROGRESS_KEY, text);
            PlayerPrefs.Save();
        }

        private bool HaveSavedProgress()
        {
            return PlayerPrefs.HasKey(PROGRESS_KEY);
        }
    }
}