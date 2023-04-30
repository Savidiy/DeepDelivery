using UnityEngine;

namespace MainModule
{
    public class QuestGiver
    {
        private readonly QuestGiveBehaviour _data;
        private readonly QuestFactory _questFactory;

        private bool _isQuestGave;

        public QuestGiver(QuestGiveBehaviour data, QuestFactory questFactory)
        {
            _data = data;
            _questFactory = questFactory;
        }

        public bool CanGiveQuest(Player player)
        {
            if (_isQuestGave)
                return false;

            Vector3 playerPosition = player.Position;
            Vector3 position = _data.transform.position;
            position.z = playerPosition.z;
            float distance = Vector3.Distance(playerPosition, position);
            float interactRadius = _data.InteractRadius;

            if (distance > interactRadius)
                return false;

            return true;
        }

        public void GiveQuest(Player player)
        {
            _isQuestGave = true;
            Quest quest = _questFactory.Create(this);
            player.AddQuest(quest);
        }

        public bool IsQuestDestination(QuestTakeBehaviour questTakeBehaviour)
        {
            return _data.QuestTakeBehaviour == questTakeBehaviour;
        }
    }
}