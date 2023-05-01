namespace MainModule
{
    public interface IEnemyMover
    {
        void UpdatePosition(float deltaTime);
        EnemyMoveProgress GetProgress();
        void LoadProgress(EnemyMoveProgress progress);
    }
}