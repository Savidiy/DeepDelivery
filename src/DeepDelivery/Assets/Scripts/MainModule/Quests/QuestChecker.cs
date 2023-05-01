using System.Collections.Generic;
using Savidiy.Utils;

namespace MainModule
{
    public class QuestChecker : IProgressWriter
    {
        private readonly LevelHolder _levelHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public QuestChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder,
            ProgressUpdater progressUpdater)
        {
            _levelHolder = levelHolder;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;

            progressUpdater.Register(this);
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        public void LoadProgress(Progress progress)
        {
            LevelModel levelModel = _levelHolder.LevelModel;

            foreach (QuestGiver questGiver in levelModel.QuestGivers)
                if (NeedCompleteQuest(progress, questGiver))
                    questGiver.SetQuestCompleted();

            Player player = _playerHolder.Player;
            foreach (QuestGiver questGiver in levelModel.QuestGivers)
                if (NeedGiveQuest(progress, questGiver))
                    questGiver.GiveQuest(player);
        }

        private static bool NeedGiveQuest(Progress progress, QuestGiver questGiver)
        {
            if (progress.TookQuestsId == null)
                return false;

            return !questGiver.IsQuestComplete && progress.TookQuestsId.Contains(questGiver.Id);
        }

        private static bool NeedCompleteQuest(Progress progress, QuestGiver questGiver)
        {
            if (progress.CompletedQuestsId == null)
                return false;

            return progress.CompletedQuestsId.Contains(questGiver.Id);
        }

        public void UpdateProgress(Progress progress)
        {
            progress.CompletedQuestsId = new List<string>();
            progress.TookQuestsId = new List<string>();

            LevelModel levelModel = _levelHolder.LevelModel;
            foreach (QuestGiver questGiver in levelModel.QuestGivers)
            {
                if (questGiver.IsQuestComplete)
                    progress.CompletedQuestsId.Add(questGiver.Id);
                else if (questGiver.IsQuestGave)
                    progress.TookQuestsId.Add(questGiver.Id);
            }
        }

        private void OnUpdated()
        {
            Player player = _playerHolder.Player;

            LevelModel levelModel = _levelHolder.LevelModel;
            foreach (QuestGiver questGiver in levelModel.QuestGivers)
                if (questGiver.CanGiveQuest(player))
                    questGiver.GiveQuest(player);

            foreach (QuestTaker questTaker in levelModel.QuestTakers)
                if (questTaker.CanTakeQuest(player))
                    questTaker.TakeQuest(player);
        }
    }
}