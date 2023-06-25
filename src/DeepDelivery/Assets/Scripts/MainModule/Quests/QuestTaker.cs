using UnityEngine;

namespace MainModule
{
    public class QuestTaker
    {
        private readonly QuestTakeBehaviour _behaviour;

        public QuestTaker(QuestTakeBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public bool CanTakeQuest(Player player)
        {
            Vector3 playerPosition = player.Position;
            Vector3 position = _behaviour.transform.position;
            position.z = playerPosition.z;
            float distance = Vector3.Distance(playerPosition, position);
            float interactRadius = _behaviour.InteractRadius;

            if (distance > interactRadius)
                return false;
            
            foreach (Quest quest in player.Quests)
            {
                if (quest.IsQuestDestination(_behaviour))
                    return true;
            }

            return false;
        }

        public void TakeQuest(Player player)
        {
            for (var index = player.Quests.Count - 1; index >= 0; index--)
            {
                Quest quest = player.Quests[index];
                if (!quest.IsQuestDestination(_behaviour))
                    continue;
                
                quest.SetQuestCompleted();
                player.RemoveQuest(quest);
            }
        }
    }
}