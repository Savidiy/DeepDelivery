using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    public class Shop : IProgressWriter
    {
        private readonly ShopBehaviour _shopBehaviour;
        private bool _isSoldOut;

        public ItemType ItemType => _shopBehaviour.PriceItemType;

        public Shop(ShopBehaviour shopBehaviour)
        {
            _shopBehaviour = shopBehaviour;
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

        public bool CanPlayerBuy(Player player)
        {
            if (_isSoldOut)
                return false;

            if (!IsPlayerOnInteractDistance(player))
                return false;

            if (!player.ItemsCount.TryGetValue(_shopBehaviour.PriceItemType, out int count))
                return false;

            if (count < _shopBehaviour.PriceCount)
                return false;

            return true;
        }

        private bool IsPlayerOnInteractDistance(Player player)
        {
            Vector3 playerPosition = player.Position;
            Vector3 shopPosition = _shopBehaviour.transform.position;
            shopPosition.z = playerPosition.z;

            float distance = Vector3.Distance(playerPosition, shopPosition);
            float interactRadius = _shopBehaviour.InteractRadius;

            return distance <= interactRadius;
        }

        public bool CanPlayerTrack(Player player)
        {
            if (_isSoldOut)
                return false;

            return IsPlayerOnInteractDistance(player);
        }

        public void PlayerBuy(Player player)
        {
            player.RemoveItems(_shopBehaviour.PriceItemType, _shopBehaviour.PriceCount);
            player.AddGun(_shopBehaviour.SellingGunType);
            SetSoldOut(true);
        }

        private void SetSoldOut(bool isSoldOut)
        {
            _isSoldOut = isSoldOut;
            _shopBehaviour.SetHasItem(!isSoldOut);
        }
    }
}