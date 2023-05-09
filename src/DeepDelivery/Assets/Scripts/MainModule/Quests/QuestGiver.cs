using System;
using UnityEngine;

namespace MainModule
{
    public class QuestGiver
    {
        private readonly QuestGiveBehaviour _data;
        private readonly QuestFactory _questFactory;

        public bool IsQuestGave { get; private set; }
        public bool IsQuestComplete { get; private set; }
        public string Id => _data.UniqueId.Id;

        public event Action StatusUpdated;

        public QuestGiver(QuestGiveBehaviour data, QuestFactory questFactory)
        {
            _data = data;
            _questFactory = questFactory;
            _data.OrderLabel.SetActive(true);
            _data.QuestTakeBehaviour.OrderLabel.SetActive(false);
        }

        public bool CanGiveQuest(Player player)
        {
            if (IsQuestGave)
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
            IsQuestGave = true;
            IsQuestComplete = false;
            _data.OrderLabel.SetActive(false);
            _data.QuestTakeBehaviour.OrderLabel.SetActive(true);
            Quest quest = _questFactory.Create(this);
            player.AddQuest(quest);
            StatusUpdated?.Invoke();
        }

        public bool IsQuestDestination(QuestTakeBehaviour questTakeBehaviour)
        {
            return _data.QuestTakeBehaviour == questTakeBehaviour;
        }

        public Vector3 GetTargetPosition()
        {
            return _data.QuestTakeBehaviour.transform.position;
        }

        public void SetQuestCompleted()
        {
            _data.OrderLabel.SetActive(false);
            _data.QuestTakeBehaviour.OrderLabel.SetActive(false);
            IsQuestGave = true;
            IsQuestComplete = true;
            StatusUpdated?.Invoke();
        }
    }
}