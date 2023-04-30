using System;
using MvvmModule;
using Object = UnityEngine.Object;

namespace LevelWindowModule.View
{
    public sealed class QuestStatusView : View<QuestStatusHierarchy, IQuestStatusViewModel>
    {
        public QuestStatusView(QuestStatusHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(IQuestStatusViewModel viewModel)
        {
            Bind(viewModel.QuestStatus, OnStatusChange);
        }

        private void OnStatusChange(QuestStatus status)
        {
            Hierarchy.Image.sprite = status switch
            {
                QuestStatus.NotStarted => Hierarchy.NotStarted,
                QuestStatus.InProcess => Hierarchy.InProcess,
                QuestStatus.Completed => Hierarchy.Completed,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        public override void Dispose()
        {
            base.Dispose();
            if (Hierarchy != null)
                Object.Destroy(Hierarchy.gameObject);
        }
    }
}