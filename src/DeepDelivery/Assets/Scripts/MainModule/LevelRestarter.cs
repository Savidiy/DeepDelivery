namespace MainModule
{
    public sealed class LevelRestarter
    {
        private readonly PlayerHolder _playerHolder;
        private readonly LevelHolder _levelHolder;

        public LevelRestarter(PlayerHolder playerHolder, LevelHolder levelHolder) 
        {
            _playerHolder = playerHolder;
            _levelHolder = levelHolder;
        }

        public void RestartLevel()
        {
            _levelHolder.LoadCurrentLevel();
            _playerHolder.CreatePlayer();
        }
    }
}