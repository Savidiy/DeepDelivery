using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Savidiy.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class ItemSpawnPoint : IDisposable, IProgressWriter
    {
        private readonly ItemSpawnPointBehaviour _behaviour;
        private readonly ItemBehaviourFactory _itemBehaviourFactory;
        private readonly PlayerInventory _playerInventory;

        [CanBeNull] private ItemBehaviour _itemBehaviour;

        public bool IsCollected { get; private set; }
        public ItemType ItemType => _behaviour.ItemType;
        public Vector3 Position => _behaviour.transform.position;

        public ItemSpawnPoint(ItemSpawnPointBehaviour behaviour, ItemBehaviourFactory itemBehaviourFactory,
            PlayerInventory playerInventory)
        {
            _behaviour = behaviour;
            _itemBehaviourFactory = itemBehaviourFactory;
            _playerInventory = playerInventory;
        }

        public void LoadProgress(Progress progress)
        {
            IsCollected = progress.CollectedItemId?.Contains(_behaviour.UniqueId.Id) ?? false;
            UpdateBehaviour();
        }

        public void UpdateProgress(Progress progress)
        {
            progress.CollectedItemId ??= new List<string>();

            string id = _behaviour.UniqueId.Id;
            bool contains = progress.CollectedItemId.Contains(id);

            if (contains && !IsCollected)
                progress.CollectedItemId.Remove(id);
            else if (!contains && IsCollected)
                progress.CollectedItemId.Add(id);
        }

        public void Collect()
        {
            IsCollected = true;
            DestroyBehaviour();
            _playerInventory.AddItem(_behaviour.ItemType);
        }

        public bool CanBeCollect(PlayerVisual playerVisual)
        {
            if (IsCollected)
                return false;

            bool hasCollision = _itemBehaviour.Collider.HasCollisionWith(playerVisual.Collider);
            return hasCollision;
        }

        public void Dispose()
        {
            DestroyBehaviour();
        }

        private void UpdateBehaviour()
        {
            if (IsCollected && _itemBehaviour != null)
            {
                DestroyBehaviour();
            }
            else if (!IsCollected && _itemBehaviour == null)
            {
                ItemType itemType = _behaviour.ItemType;
                Vector3 position = _behaviour.transform.position;
                _itemBehaviour = _itemBehaviourFactory.Create(itemType, position);
            }
        }

        private void DestroyBehaviour()
        {
            if (_itemBehaviour != null)
                Object.Destroy(_itemBehaviour.gameObject);

            _itemBehaviour = null;
        }
    }
}