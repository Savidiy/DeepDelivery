using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    public class Shop : IProgressWriter
    {
        private readonly ShopBehaviour _shopBehaviour;
        private readonly PlayerInventory _playerInventory;
        private readonly PlayerGunHandler _playerGunHandler;
        private bool _isSoldOut;

        public ItemType ItemType => _shopBehaviour.PriceItemType;

        public Shop(ShopBehaviour shopBehaviour, PlayerInventory playerInventory, PlayerGunHandler playerGunHandler)
        {
            _shopBehaviour = shopBehaviour;
            _playerInventory = playerInventory;
            _playerGunHandler = playerGunHandler;
        }

        public void LoadProgress(Progress progress)
        {
            bool isSoldOut = progress.SoldOutShopsId?.Contains(_shopBehaviour.UniqueId.Id) ?? false;
            SetSoldOut(isSoldOut);
        }

        public void UpdateProgress(Progress progress)
        {
            progress.SoldOutShopsId ??= new List<string>();

            string id = _shopBehaviour.UniqueId.Id;
            bool contains = progress.SoldOutShopsId.Contains(id);
            if (contains && !_isSoldOut)
                progress.SoldOutShopsId.Remove(id);
            else if (!contains && _isSoldOut)
                progress.SoldOutShopsId.Add(id);
        }

        public bool CanPlayerBuy(PlayerVisual playerVisual)
        {
            if (_isSoldOut)
                return false;

            if (!IsPlayerOnInteractDistance(playerVisual))
                return false;

            if (!_playerInventory.ItemsCount.TryGetValue(_shopBehaviour.PriceItemType, out int count))
                return false;

            if (count < _shopBehaviour.PriceCount)
                return false;

            return true;
        }

        private bool IsPlayerOnInteractDistance(PlayerVisual playerVisual)
        {
            Vector3 playerPosition = playerVisual.Position;
            Vector3 shopPosition = _shopBehaviour.transform.position;
            shopPosition.z = playerPosition.z;

            float distance = Vector3.Distance(playerPosition, shopPosition);
            float interactRadius = _shopBehaviour.InteractRadius;

            return distance <= interactRadius;
        }

        public bool CanPlayerTrack(PlayerVisual playerVisual)
        {
            if (_isSoldOut)
                return false;

            return IsPlayerOnInteractDistance(playerVisual);
        }

        public void PlayerBuy(PlayerVisual playerVisual)
        {
            _playerInventory.RemoveItems(_shopBehaviour.PriceItemType, _shopBehaviour.PriceCount);
            _playerGunHandler.AddGun(_shopBehaviour.SellingGunType);
            playerVisual.UpdateGunVisibility();
            SetSoldOut(true);
        }

        private void SetSoldOut(bool isSoldOut)
        {
            _isSoldOut = isSoldOut;
            _shopBehaviour.SetHasItem(!isSoldOut);
        }
    }
}