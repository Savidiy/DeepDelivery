using MvvmModule;
using UniRx;

namespace LevelWindowModule.View
{
    public interface IQuestStatusViewModel : IViewModel
    {
        IReadOnlyReactiveProperty<QuestStatus> QuestStatus { get; }
    }
}