namespace MainModule
{
    public class ItemSpawnPointFactory : IFactory<ItemSpawnPoint, ItemSpawnPointBehaviour>
    {
        private readonly ItemBehaviourFactory _itemBehaviourFactory;

        public ItemSpawnPointFactory(ItemStaticDataProvider itemStaticDataProvider, ItemBehaviourFactory itemBehaviourFactory)
        {
            _itemBehaviourFactory = itemBehaviourFactory;
        }
        
        public ItemSpawnPoint Create(ItemSpawnPointBehaviour itemSpawnPointBehaviour)
        {
            var item = new ItemSpawnPoint(itemSpawnPointBehaviour, _itemBehaviourFactory);
            return item;
        }
    }
}