using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class ItemCompassUpdater
    {
        private const string PREFAB_NAME = "ItemCompass";

        private readonly IPrefabFactory _prefabFactory;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly Transform _root;
        private readonly List<ItemCompassBehaviour> _compasses = new();
        private readonly GameStaticData _gameStaticData;
        private readonly TrackedItemsHolder _trackedItemsHolder;
        private readonly ItemStaticDataProvider _itemStaticDataProvider;
        private readonly LevelHolder _levelHolder;

        public ItemCompassUpdater(TickInvoker tickInvoker, PlayerHolder playerHolder, IPrefabFactory prefabFactory,
            GameStaticData gameStaticData, TrackedItemsHolder trackedItemsHolder, ItemStaticDataProvider itemStaticDataProvider,
            LevelHolder levelHolder)
        {
            _gameStaticData = gameStaticData;
            _trackedItemsHolder = trackedItemsHolder;
            _itemStaticDataProvider = itemStaticDataProvider;
            _levelHolder = levelHolder;
            _prefabFactory = prefabFactory;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;

            _root = new GameObject("ItemCompassRoot").transform;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            UpdateCompassCount();
            UpdateCompassesPosition();
        }

        private void UpdateCompassCount()
        {
            int count = _trackedItemsHolder.TrackedItems.Count;

            for (int i = _compasses.Count - 1; i >= count; i--)
            {
                Object.Destroy(_compasses[i].gameObject);
                _compasses.RemoveAt(i);
            }

            for (int i = _compasses.Count; i < count; i++)
            {
                var compassBehaviour = _prefabFactory.Instantiate<ItemCompassBehaviour>(PREFAB_NAME, _root);
                _compasses.Add(compassBehaviour);
            }
        }

        private void UpdateCompassesPosition()
        {
            PlayerVisual playerVisual = _playerHolder.PlayerVisual;
            Vector3 playerPosition = playerVisual.Position;
            IReadOnlyList<ItemType> trackedItem = _trackedItemsHolder.TrackedItems;

            for (var index = 0; index < trackedItem.Count; index++)
            {
                ItemType itemType = trackedItem[index];
                bool hasTarget = TryFindTargetPosition(itemType, playerPosition, out Vector3 targetPosition);
                ItemCompassBehaviour itemCompassBehaviour = _compasses[index];
                UpdateSprite(itemCompassBehaviour, itemType, hasTarget);
                UpdateCompass(playerPosition, targetPosition, itemCompassBehaviour);
            }
        }

        private void UpdateSprite(ItemCompassBehaviour itemCompassBehaviour, ItemType itemType, bool hasTarget)
        {
            itemCompassBehaviour.gameObject.SetActive(hasTarget);
            itemCompassBehaviour.SpriteRenderer.sprite = _itemStaticDataProvider.GetData(itemType).Sprite;
        }

        private bool TryFindTargetPosition(ItemType itemType, Vector3 playerPosition, out Vector3 targetPosition)
        {
            var minSqrDistance = float.MaxValue;
            targetPosition = Vector3.zero;

            foreach (ItemSpawnPoint itemSpawnPoint in _levelHolder.LevelModel.Items)
            {
                bool isSuitableItem = itemSpawnPoint.ItemType == itemType && !itemSpawnPoint.IsCollected;
                if (!isSuitableItem)
                    continue;

                Vector3 position = itemSpawnPoint.Position;
                float sqrMagnitude = Vector3.SqrMagnitude(position - playerPosition);
                if (sqrMagnitude >= minSqrDistance)
                    continue;

                minSqrDistance = sqrMagnitude;
                targetPosition = position;
            }

            bool wasFounded = minSqrDistance != float.MaxValue;
            return wasFounded;
        }

        private void UpdateCompass(Vector3 playerPosition, Vector3 targetPosition, ItemCompassBehaviour itemCompassBehaviour)
        {
            targetPosition.z = playerPosition.z;
            Vector3 forward = targetPosition - playerPosition;
            forward.Normalize();

            float compassDrawDistance = _gameStaticData.ItemCompassDrawDistance;
            Vector3 shift = forward * compassDrawDistance;

            Vector3 position = playerPosition + shift;

            ItemCompassBehaviour questCompassBehaviour = itemCompassBehaviour;
            Transform transform = questCompassBehaviour.transform;
            transform.position = position;
        }
    }
}