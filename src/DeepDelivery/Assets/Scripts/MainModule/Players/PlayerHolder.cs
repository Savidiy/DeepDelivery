namespace MainModule
{
    public class PlayerHolder
    {
        public PlayerVisual PlayerVisual { get; private set; }

        public void AddPlayer(PlayerVisual playerVisual)
        {
            PlayerVisual = playerVisual;
        }

        public void RemovePlayer()
        {
            PlayerVisual?.Dispose();
            PlayerVisual = null;
        }
    }
}