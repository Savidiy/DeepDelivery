using System.Collections.Generic;
using MvvmModule;
using UniRx;

namespace LevelWindowModule.View
{
    public interface ILevelWindowViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<int> HeartCount { get; }
        IReadOnlyReactiveProperty<IItemsViewModel> Items { get; }
        IReadOnlyList<IQuestStatusViewModel> Quests { get; }

        void SettingsClickFromView();
        void RestartLevelClickFromView();
        void LoadLevelClickFromView();
    }
}