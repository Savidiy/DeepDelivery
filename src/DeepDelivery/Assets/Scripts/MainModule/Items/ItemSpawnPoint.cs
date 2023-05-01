using System;
using System.Collections.Generic;
using Savidiy.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class ItemSpawnPoint : IDisposable, IProgressWriter
    {
        private readonly ItemSpawnPointBehaviour _data;
        private readonly ItemBehaviourFactory _itemBehaviourFactory;

        private ItemBehaviour _itemBehaviour;
        private bool _isCollected;

        public ItemSpawnPoint(ItemSpawnPointBehaviour data, ItemBehaviourFactory itemBehaviourFactory)
        {
            _data = data;
            _itemBehaviourFactory = itemBehaviourFactory;
        }

        public void LoadProgress(Progress progress)
        {
            _isCollected = progress.CollectedItemId?.Contains(_data.UniqueId.Id) ?? false;
            UpdateBehaviour();
        }

        public void UpdateProgress(Progress progress)
        {
            progress.CollectedItemId ??= new List<string>();
            
            string id = _data.UniqueId.Id;
            bool contains = progress.CollectedItemId.Contains(id);

            if (contains && !_isCollected)
                progress.CollectedItemId.Remove(id);
            else if (!contains && _isCollected)
                progress.CollectedItemId.Add(id);
        }

        public void Collect(Player player)
        {
            _isCollected = true;
            DestroyBehaviour();
            player.AddItem(_data.ItemType);
        }

        public bool CanBeCollect(Player player)
        {
            if (_isCollected)
                return false;

            bool hasCollision = _itemBehaviour.Collider.HasCollisionWith(player.Collider);
            return hasCollision;
        }

        public void Dispose()
        {
            DestroyBehaviour();
        }

        private void UpdateBehaviour()
        {
            if (_isCollected && _itemBehaviour != null)
            {
                DestroyBehaviour();
            }
            else if (!_isCollected && _itemBehaviour == null)
            {
                ItemType itemType = _data.ItemType;
                Vector3 position = _data.transform.position;
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