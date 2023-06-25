using System.Collections.Generic;

namespace MainModule
{
    public class QuestFactory
    {
        private readonly PlayerQuestsHandler _playerQuestsHandler;

        public QuestFactory(PlayerQuestsHandler playerQuestsHandler)
        {
            _playerQuestsHandler = playerQuestsHandler;
        }
        
        public Quest Create(QuestGiver data)
        {
            return new Quest(data);
        }

        public List<QuestGiver> CreateQuestGivers(List<QuestGiveBehaviour> questGiveBehaviours)
        {
            var questGivers = new List<QuestGiver>();
            foreach (QuestGiveBehaviour behaviour in questGiveBehaviours)
                questGivers.Add(Create(behaviour));

            return questGivers;
        }

        public List<QuestTaker> CreateQuestTakers(List<QuestTakeBehaviour> questTakeBehaviours)
        {
            var questTakers = new List<QuestTaker>();
            foreach (QuestTakeBehaviour behaviour in questTakeBehaviours)
                questTakers.Add(Create(behaviour));

            return questTakers;
        }

        private QuestGiver Create(QuestGiveBehaviour data)
        {
            return new QuestGiver(data, this, _playerQuestsHandler);
        }

        private QuestTaker Create(QuestTakeBehaviour data)
        {
            return new QuestTaker(data, _playerQuestsHandler);
        }
    }
}