namespace MainModule
{
    public sealed class PlayerHealth : IProgressWriter
    {
        public int CurrentHp { get; private set; }
        private int _maxHp;

        public PlayerHealth(ProgressUpdater progressUpdater)
        {
            progressUpdater.Register(this);
        }

        public void GetHit()
        {
            CurrentHp--;
        }

        public void AddHeart()
        {
            _maxHp++;
            CurrentHp = _maxHp;
        }

        public void LoadProgress(Progress progress)
        {
            int maxHp = progress.Player.MaxHp;
            CurrentHp = maxHp;
            _maxHp = maxHp;
        }

        public void UpdateProgress(Progress progress)
        {
            progress.Player.MaxHp = _maxHp;
            progress.Player.CurrentHp = CurrentHp;
        }
    }
}