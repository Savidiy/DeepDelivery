using UnityEngine;

namespace MainModule
{
    public class Shop
    {
        private readonly ShopBehaviour _shopBehaviour;
        private bool _isSoldOut;

        public Shop(ShopBehaviour shopBehaviour)
        {
            _shopBehaviour = shopBehaviour;
            _shopBehaviour.SetHasItem(true);
        }

        public bool CanPlayerBuy(Player player)
        {
            if (_isSoldOut)
                return false;
            
            Vector3 playerPosition = player.Position;
            Vector3 shopPosition = _shopBehaviour.transform.position;
            shopPosition.z = playerPosition.z;

            float distance = Vector3.Distance(playerPosition, shopPosition);
            float interactRadius = _shopBehaviour.InteractRadius;

            if (distance > interactRadius)
                return false;

            if (!player.ItemsCount.TryGetValue(_shopBehaviour.PriceItemType, out int count))
                return false;

            if (count < _shopBehaviour.PriceCount)
                return false;

            return true;
        }

        public void PlayerBuy(Player player)
        {
            player.RemoveItems(_shopBehaviour.PriceItemType, _shopBehaviour.PriceCount);
            player.AddGun(_shopBehaviour.SellingGunType);
            _isSoldOut = true;
            _shopBehaviour.SetHasItem(false);
        }
    }
}