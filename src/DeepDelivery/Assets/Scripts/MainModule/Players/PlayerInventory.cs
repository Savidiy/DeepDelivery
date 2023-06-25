using System.Collections.Generic;

namespace MainModule
{
    public sealed class PlayerInventory : IProgressWriter
    {
        private readonly PlayerHealth _playerHealth;
        
        public Dictionary<ItemType, int> ItemsCount { get; } = new();

        public PlayerInventory(PlayerHealth playerHealth, ProgressUpdater progressUpdater)
        {
            _playerHealth = playerHealth;
            progressUpdater.Register(this);
        }
        
        public void LoadProgress(Progress progress)
        {
            ItemsCount.Clear();
            if (progress.Player.Items == null)
                return;

            foreach (ItemType itemType in progress.Player.Items)
            {
                ItemsCount.TryAdd(itemType, 0);
                ItemsCount[itemType] += 1;
            }
        }

        public void UpdateProgress(Progress progress)
        {
            PlayerProgress playerProgress = progress.Player;
            playerProgress.Items = new List<ItemType>();
            foreach ((ItemType key, int count) in ItemsCount)
                for (int i = 0; i < count; i++)
                    playerProgress.Items.Add(key);
        }

        public void AddItem(ItemType itemType)
        {
            if (itemType == ItemType.Heart)
            {
                _playerHealth.AddHeart();
                return;
            }

            if (ItemsCount.TryGetValue(itemType, out var count))
                ItemsCount[itemType] = count + 1;
            else
                ItemsCount.Add(itemType, 1);
        }

        public void RemoveItems(ItemType itemType, int count)
        {
            ItemsCount[itemType] -= count;
        }
    }
}