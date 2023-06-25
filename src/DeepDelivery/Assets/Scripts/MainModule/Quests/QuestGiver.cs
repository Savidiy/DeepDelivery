using System;
using UnityEngine;

namespace MainModule
{
    public class QuestGiver
    {
        private readonly QuestGiveBehaviour _behaviour;
        private readonly QuestFactory _questFactory;

        public bool IsQuestGave { get; private set; }
        public bool IsQuestComplete { get; private set; }
        public string Id => _behaviour.UniqueId.Id;

        public event Action StatusUpdated;

        public QuestGiver(QuestGiveBehaviour behaviour, QuestFactory questFactory)
        {
            _behaviour = behaviour;
            _questFactory = questFactory;
            _behaviour.OrderLabel.SetActive(true);
            _behaviour.QuestTakeBehaviour.OrderLabel.SetActive(false);
        }

        public bool CanGiveQuest(Player player)
        {
            if (IsQuestGave)
                return false;

            Vector3 playerPosition = player.Position;
            Vector3 position = _behaviour.transform.position;
            position.z = playerPosition.z;
            float distance = Vector3.Distance(playerPosition, position);
            float interactRadius = _behaviour.InteractRadius;

            if (distance > interactRadius)
                return false;

            return true;
        }

        public void GiveQuest(Player player)
        {
            IsQuestGave = true;
            IsQuestComplete = false;
            _behaviour.OrderLabel.SetActive(false);
            _behaviour.QuestTakeBehaviour.OrderLabel.SetActive(true);
            Quest quest = _questFactory.Create(this);
            player.AddQuest(quest);
            StatusUpdated?.Invoke();
        }

        public bool IsQuestDestination(QuestTakeBehaviour questTakeBehaviour)
        {
            return _behaviour.QuestTakeBehaviour == questTakeBehaviour;
        }

        public Vector3 GetTargetPosition()
        {
            return _behaviour.QuestTakeBehaviour.transform.position;
        }

        public void SetQuestCompleted()
        {
            _behaviour.OrderLabel.SetActive(false);
            _behaviour.QuestTakeBehaviour.OrderLabel.SetActive(false);
            IsQuestGave = true;
            IsQuestComplete = true;
            StatusUpdated?.Invoke();
        }
    }
}