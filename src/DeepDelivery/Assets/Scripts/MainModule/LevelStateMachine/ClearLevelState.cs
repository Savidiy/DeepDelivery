using Savidiy.Utils.StateMachine;

namespace MainModule
{
    public class ClearLevelStateArgs
    {
        public bool NeedLoadAfterClear { get; }

        public ClearLevelStateArgs(bool needLoadAfterClear)
        {
            NeedLoadAfterClear = needLoadAfterClear;
        }
    }

    public sealed class ClearLevelState : IStateWithPayload<ClearLevelStateArgs>, ILevelState
    {
        private readonly CollisionWithEnemyChecker _collisionWithEnemyChecker;
        private readonly BulletHolder _bulletHolder;
        private readonly LevelStateMachine _levelStateMachine;
        private readonly PlayerHolder _playerHolder;
        private readonly LevelHolder _levelHolder;
        private readonly PlayerQuestsHandler _playerQuestsHandler;
        private readonly EnemyHolder _enemyHolder;

        public ClearLevelState(CollisionWithEnemyChecker collisionWithEnemyChecker, BulletHolder bulletHolder,
            LevelStateMachine levelStateMachine, PlayerHolder playerHolder, LevelHolder levelHolder,
            PlayerQuestsHandler playerQuestsHandler, EnemyHolder enemyHolder)
        {
            _levelStateMachine = levelStateMachine;
            _playerHolder = playerHolder;
            _levelHolder = levelHolder;
            _playerQuestsHandler = playerQuestsHandler;
            _enemyHolder = enemyHolder;
            _collisionWithEnemyChecker = collisionWithEnemyChecker;
            _bulletHolder = bulletHolder;
        }

        public void Enter(ClearLevelStateArgs payload)
        {
            _collisionWithEnemyChecker.ClearCollisions();
            _playerQuestsHandler.ClearQuests();
            _enemyHolder.Clear();
            _bulletHolder.ClearBullets();
            _playerHolder.RemovePlayer();
            _levelHolder.RemoveLevel();

            if (payload.NeedLoadAfterClear)
                _levelStateMachine.EnterToState<LoadLevelState>();
        }
    }
}