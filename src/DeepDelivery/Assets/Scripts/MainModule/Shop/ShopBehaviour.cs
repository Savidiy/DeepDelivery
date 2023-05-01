using UnityEngine;

namespace MainModule
{
    public class ShopBehaviour : MonoBehaviour
    {
        public float InteractRadius = 2f;
        public GunType SellingGunType;
        public ItemType PriceItemType;
        public int PriceCount = 1;
        public GameObject HasItemGroup;
        public GameObject SoldOutGroup;
        public GameObject Number1;
        public GameObject Number2;
        public GameObject Number3;
        public GameObject Number4;
        public GameObject PriceDiamond;
        public GameObject PriceChest;
        public UniqueId UniqueId;

        public void SetHasItem(bool hasItem)
        {
            HasItemGroup.SetActive(hasItem);
            SoldOutGroup.SetActive(!hasItem);
        }

        private void OnValidate()
        {
            name = $"Shop - {SellingGunType} = {PriceCount} x {PriceItemType}";
            
            PriceDiamond.SetActive(PriceItemType == ItemType.Diamond);
            PriceChest.SetActive(PriceItemType == ItemType.Chest);
            Number1.SetActive(PriceCount == 1);
            Number2.SetActive(PriceCount == 2);
            Number3.SetActive(PriceCount == 3);
            Number4.SetActive(PriceCount == 4);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, InteractRadius);
        }
    }
}