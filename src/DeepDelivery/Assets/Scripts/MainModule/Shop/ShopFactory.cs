namespace MainModule
{
    public class ShopFactory
    {
        public Shop Create(ShopBehaviour shopBehaviour)
        {
            return new Shop(shopBehaviour);
        }
    }
}