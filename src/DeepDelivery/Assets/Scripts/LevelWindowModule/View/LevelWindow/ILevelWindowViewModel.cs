using System.Collections.Generic;
using MvvmModule;
using UniRx;
using UnityEngine;

namespace LevelWindowModule.View
{
    public interface ILevelWindowViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<int> HeartCount { get; }
        IReadOnlyReactiveProperty<IItemsViewModel> Items { get; }
        IReadOnlyList<IQuestStatusViewModel> Quests { get; }
        IReadOnlyReactiveProperty<bool> UseMobileInput { get; }

        void SettingsClickFromView();
        void RestartLevelClickFromView();
        void LoadLevelClickFromView();
        void SetMobileInputFromView(Vector2 inputDirection, bool isPressedValue);
    }
}