using Savidiy.Utils;

namespace MainModule
{
    public class QuestChecker
    {
        private readonly LevelHolder _levelHolder;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;

        public QuestChecker(TickInvoker tickInvoker, PlayerHolder playerHolder, LevelHolder levelHolder)
        {
            _levelHolder = levelHolder;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
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

        private void OnUpdated()
        {
            Player player = _playerHolder.Player;

            LevelModel levelModel = _levelHolder.LevelModel;
            foreach (QuestGiver questGiver in levelModel.QuestGivers)
                if (questGiver.CanGiveQuest(player))
                    questGiver.GiveQuest(player);
        
            foreach (QuestTaker questTaker in levelModel.QuestTakers)
                if (questTaker.CanTakeQuest(player))
                    questTaker.TakeQuest(player);}
    }
}