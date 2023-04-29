using MvvmModule;

namespace MainModule
{
    public sealed class PlayerFactory
    {
        private readonly IPrefabFactory _prefabFactory;

        public PlayerFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }
        
        public Player CreatePlayer()
        {
            var playerBehaviour = _prefabFactory.Instantiate<PlayerBehaviour>("Player", parent: null);

            var player = new Player(playerBehaviour);
            return player;
        }
    }
}