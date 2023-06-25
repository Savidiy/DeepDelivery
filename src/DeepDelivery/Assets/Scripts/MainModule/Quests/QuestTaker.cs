using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    public class QuestTaker
    {
        private readonly QuestTakeBehaviour _behaviour;
        private readonly PlayerQuestsHandler _playerQuestsHandler;

        public QuestTaker(QuestTakeBehaviour behaviour, PlayerQuestsHandler playerQuestsHandler)
        {
            _playerQuestsHandler = playerQuestsHandler;
            _behaviour = behaviour;
        }

        public bool CanTakeQuest(PlayerVisual playerVisual)
        {
            Vector3 playerPosition = playerVisual.Position;
            Vector3 position = _behaviour.transform.position;
            position.z = playerPosition.z;
            float distance = Vector3.Distance(playerPosition, position);
            float interactRadius = _behaviour.InteractRadius;

            if (distance > interactRadius)
                return false;
            
            foreach (Quest quest in _playerQuestsHandler.Quests)
            {
                if (quest.IsQuestDestination(_behaviour))
                    return true;
            }

            return false;
        }

        public void TakeQuest()
        {
            List<Quest> quests = _playerQuestsHandler.Quests;
            for (var index = quests.Count - 1; index >= 0; index--)
            {
                Quest quest = quests[index];
                if (!quest.IsQuestDestination(_behaviour))
                    continue;
                
                quest.SetQuestCompleted();
                _playerQuestsHandler.RemoveQuest(quest);
            }
        }
    }
}