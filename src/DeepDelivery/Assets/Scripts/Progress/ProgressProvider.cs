using System;
using SettingsModule;
using UniRx;
using UnityEngine;

namespace Progress
{
    public class ProgressProvider
    {
        private readonly GameSettings _gameSettings;
        private readonly ReactiveProperty<EControlType> _selectedControlType = new(EControlType.NotSelected);
        private const string PROGRESS_KEY = "PROGRESS_LEVEL";
        private const string CONTROLS_KEY = "SELECTED_CONTROLS";

        public int CurrentLevel { get; private set; } = 0;

        public IReadOnlyReactiveProperty<EControlType> SelectedControlType => _selectedControlType;

        public bool HasNextLevel => CurrentLevel >= _gameSettings.Levels.Count;


        public ProgressProvider(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;

            CurrentLevel = PlayerPrefs.GetInt(PROGRESS_KEY, 0);
            _selectedControlType.Value = (EControlType) PlayerPrefs.GetInt(CONTROLS_KEY, (int) EControlType.NotSelected);
        }

        public void OpenNextLevel()
        {
            CurrentLevel++;
            PlayerPrefs.SetInt(PROGRESS_KEY, CurrentLevel);
            PlayerPrefs.Save();

            Debug.Log($"Current Level '{CurrentLevel}'");
        }

        public void ResetProgress()
        {
            CurrentLevel = 0;
            PlayerPrefs.SetInt(PROGRESS_KEY, CurrentLevel);
            PlayerPrefs.Save();

            Debug.Log($"Reset progress");
        }

        public void SetControls(EControlType controlType)
        {
            _selectedControlType.Value = controlType;
            PlayerPrefs.SetInt(CONTROLS_KEY, (int) _selectedControlType.Value);
            PlayerPrefs.Save();
        }
    }
}