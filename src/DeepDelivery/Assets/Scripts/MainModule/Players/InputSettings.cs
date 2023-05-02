using UniRx;
using UnityEngine;

namespace MainModule
{
    public sealed class InputSettings
    {
        private const string CONTROLS_KEY = "SELECTED_CONTROLS";
        
        private readonly ReactiveProperty<EControlType> _selectedControlType = new(EControlType.NotSelected);

        public IReadOnlyReactiveProperty<EControlType> SelectedControlType => _selectedControlType;

        public InputSettings()
        {
            _selectedControlType.Value = (EControlType) PlayerPrefs.GetInt(CONTROLS_KEY, (int) EControlType.Mobile);
        }

        public void SetControls(EControlType controlType)
        {
            _selectedControlType.Value = controlType;
            PlayerPrefs.SetInt(CONTROLS_KEY, (int) _selectedControlType.Value);
            PlayerPrefs.Save();
        }
    }
}