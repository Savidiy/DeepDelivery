using System.Collections.Generic;

namespace MainModule
{
    public class ShopFactory
    {
        public List<Shop> CreateShops(List<ShopBehaviour> shopBehaviours)
        {
            var shops = new List<Shop>();
            foreach (ShopBehaviour behaviour in shopBehaviours)
                shops.Add(Create(behaviour));

            return shops;
        }

        private Shop Create(ShopBehaviour shopBehaviour)
        {
            return new Shop(shopBehaviour);
        }
    }
}