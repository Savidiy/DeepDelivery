using MvvmModule;
using UniRx;

namespace LevelWindowModule.View
{
    public interface ILevelWindowViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<int> HeartCount { get; }
        IReadOnlyReactiveProperty<IItemsViewModel> Items { get; }

        void SettingsClickFromView();
        void RestartLevelClickFromView();
    }
}