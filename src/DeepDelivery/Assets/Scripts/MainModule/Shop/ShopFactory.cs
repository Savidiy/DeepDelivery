namespace MainModule
{
    public class ShopFactory : IFactory<Shop, ShopBehaviour>
    {
        public Shop Create(ShopBehaviour shopBehaviour)
        {
            return new Shop(shopBehaviour);
        }
    }
}