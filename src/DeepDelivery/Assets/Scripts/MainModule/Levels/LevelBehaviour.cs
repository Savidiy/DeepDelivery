using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MainModule
{
    public class LevelBehaviour : MonoBehaviour
    {
        public Transform StartPoint;
        public List<EnemySpawnPointBehaviour> EnemySpawnPoints = new();
        public List<ItemSpawnPointBehaviour> ItemSpawnPoints = new();
        public List<Collider2D> Walls = new();
        public List<ShopBehaviour> Shops = new();
        public List<QuestTakeBehaviour> TakeQuests = new();
        public List<QuestGiveBehaviour> GiveQuests = new();
        public List<CheckPointBehaviour> CheckPoints = new();

        private void OnValidate()
        {
            CollectAllData();
        }

        [Button]
        public void CollectAllData()
        {
            CollectAllComponents(EnemySpawnPoints);
            CollectAllComponents(ItemSpawnPoints);
            CollectAllComponents(Shops);
            CollectAllComponents(TakeQuests);
            CollectAllComponents(GiveQuests);
            CollectAllComponents(CheckPoints);
            CollectAllComponents(Walls);
            RemoveWallsWithTrigger(Walls);

            UpdateUniqueId();
        }

        private void UpdateUniqueId()
        {
            var uniqueIds = new List<UniqueId>();
            CollectAllComponents(uniqueIds);
            for (var index = 0; index < uniqueIds.Count; index++)
            {
                UniqueId checkedUniqueId = uniqueIds[index];
                for (int prevIndex = 0; prevIndex < index; prevIndex++)
                {
                    UniqueId previousUniqueId = uniqueIds[prevIndex];
                    if (checkedUniqueId.Id.Equals(previousUniqueId.Id))
                        checkedUniqueId.GenerateId();
                }
            }
        }

        private void CollectAllComponents<T>(List<T> collection) where T : Behaviour
        {
            collection.Clear();

            var targets = new List<Transform>() {transform};

            for (var index = 0; index < targets.Count; index++)
            {
                Transform parentTransform = targets[index];
                if (parentTransform.TryGetComponent(out T col))
                    collection.Add(col);

                int childCount = parentTransform.childCount;
                for (int j = 0; j < childCount; j++)
                {
                    targets.Add(parentTransform.GetChild(j));
                }
            }
        }

        private void RemoveWallsWithTrigger(List<Collider2D> walls)
        {
            for (var index = walls.Count - 1; index >= 0; index--)
            {
                Collider2D wall = walls[index];
                if (wall.isTrigger)
                    walls.RemoveAt(index);
            }
        }
    }
}