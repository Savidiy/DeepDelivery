using UnityEngine;

namespace MainModule
{
    public class ShopBehaviour : MonoBehaviour
    {
        public float InteractRadius = 2f;
        public GunType SellingGunType;
        public ItemType PriceItemType;
        public int PriceCount = 1;
        public GameObject SoldOutLabel;

        private void OnValidate()
        {
            name = $"Shop - {SellingGunType} = {PriceCount} x {PriceItemType}";
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, InteractRadius);
        }
    }
}