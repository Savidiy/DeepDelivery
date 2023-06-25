using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    public class PlayerQuestsHandler : IProgressReader
    {
        public List<Quest> Quests { get; } = new();

        public PlayerQuestsHandler(ProgressUpdater progressUpdater)
        {
            progressUpdater.Register(this);
        }

        public void LoadProgress(Progress progress)
        {
            Quests.Clear();
        }

        public void AddQuest(Quest quest)
        {
            Quests.Add(quest);
            Debug.Log($"Took quest. Quest count = {Quests.Count}");
        }

        public void RemoveQuest(Quest quest)
        {
            Quests.Remove(quest);
            Debug.Log($"Complete quest. Quest count = {Quests.Count}");
        }
    }
}