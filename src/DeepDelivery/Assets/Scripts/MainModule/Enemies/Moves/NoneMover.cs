namespace MainModule
{
    internal class NoneMover : IEnemyMover
    {
        public void UpdatePosition(float deltaTime)
        {
        }

        public EnemyMoveProgress GetProgress()
        {
            return new EnemyMoveProgress();
        }

        public void LoadProgress(EnemyMoveProgress progress)
        {
        }
    }
}