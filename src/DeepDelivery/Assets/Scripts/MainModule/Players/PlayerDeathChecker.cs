using Savidiy.Utils;

namespace MainModule
{
    public class PlayerDeathChecker
    {
        private readonly LevelStateMachine _levelStateMachine;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHealth _playerHealth;

        public PlayerDeathChecker(TickInvoker tickInvoker, PlayerHealth playerHealth, LevelStateMachine levelStateMachine)
        {
            _levelStateMachine = levelStateMachine;
            _tickInvoker = tickInvoker;
            _playerHealth = playerHealth;
        }

        public void Activate()
        {
            _tickInvoker.Subscribe(UpdateType.Update, OnUpdated);
        }

        public void Deactivate()
        {
            _tickInvoker.Unsubscribe(UpdateType.Update, OnUpdated);
        }

        private void OnUpdated()
        {
            if (_playerHealth.CurrentHp <= 0)
                _levelStateMachine.EnterToState<DeathPauseLevelState>();
        }
    }
}