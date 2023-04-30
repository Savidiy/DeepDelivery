using LevelWindowModule.View;
using MainModule;
using MvvmModule;
using UniRx;

namespace LevelWindowModule
{
    public class QuestStatusArgs
    {
        public QuestGiver QuestGiver { get; }

        public QuestStatusArgs(QuestGiver questGiver)
        {
            QuestGiver = questGiver;
        }
    }

    public sealed class QuestStatusViewModel : ViewModel<QuestStatusArgs>, IQuestStatusViewModel
    {
        private readonly ReactiveProperty<QuestStatus> _questStatus = new(View.QuestStatus.NotStarted);
     
        public IReadOnlyReactiveProperty<QuestStatus> QuestStatus => _questStatus;

        public QuestStatusViewModel(QuestStatusArgs model, IViewModelFactory viewModelFactory) : base(model, viewModelFactory)
        {
            model.QuestGiver.StatusUpdated += OnStatusUpdated;
        }

        private void OnStatusUpdated()
        {
            if (Model.QuestGiver.IsQuestComplete)
                _questStatus.Value = View.QuestStatus.Completed;
            else if (Model.QuestGiver.IsQuestGave)
                _questStatus.Value = View.QuestStatus.InProcess;
        }

        public override void Dispose()
        {
            base.Dispose();
            Model.QuestGiver.StatusUpdated -= OnStatusUpdated;
        }
    }
}