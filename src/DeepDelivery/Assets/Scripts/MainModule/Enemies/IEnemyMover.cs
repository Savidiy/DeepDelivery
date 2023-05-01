namespace MainModule
{
    public interface IEnemyMover
    {
        LastMoveType LastMoveType { get; }
        void UpdatePosition(float deltaTime);
        EnemyMoveProgress GetProgress();
        void LoadProgress(EnemyMoveProgress progress);
    }
}