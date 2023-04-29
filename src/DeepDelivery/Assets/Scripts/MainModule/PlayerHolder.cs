namespace MainModule
{
    public class PlayerHolder
    {
        private readonly PlayerFactory _playerFactory;
        public Player Player { get; private set; }

        public PlayerHolder(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        
        public void CreatePlayer()
        {
            Player = _playerFactory.CreatePlayer();
        }
    }
}