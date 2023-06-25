using System.Collections.Generic;
using Zenject;

namespace MainModule
{
    public class ShopFactory
    {
        private readonly IInstantiator _instantiator;

        public ShopFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public List<Shop> CreateShops(List<ShopBehaviour> shopBehaviours)
        {
            var shops = new List<Shop>();
            foreach (ShopBehaviour behaviour in shopBehaviours)
                shops.Add(Create(behaviour));

            return shops;
        }

        private Shop Create(ShopBehaviour shopBehaviour)
        {
            return _instantiator.Instantiate<Shop>(new object[] {shopBehaviour});
        }
    }
}