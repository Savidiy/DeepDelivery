using DG.Tweening;
using Savidiy.Utils;

namespace MainModule
{
    public class PlayerDeathChecker
    {
        private readonly PlayerInputMover _playerInputMover;
        private readonly PlayerInputShooter _playerInputShooter;
        private readonly GameStaticData _gameStaticData;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHealth _playerHealth;
        private readonly LevelRestarter _levelRestarter;

        private Sequence _restartTween;

        public PlayerDeathChecker(TickInvoker tickInvoker, PlayerHealth playerHealth, LevelRestarter levelRestarter,
            PlayerInputMover playerInputMover, PlayerInputShooter playerInputShooter, GameStaticData gameStaticData)
        {
            _levelRestarter = levelRestarter;
            _playerInputMover = playerInputMover;
            _playerInputShooter = playerInputShooter;
            _gameStaticData = gameStaticData;
            _tickInvoker = tickInvoker;
            _playerHealth = playerHealth;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _restartTween?.Kill();
        }

        private void OnUpdated()
        {
            if (_playerHealth.CurrentHp > 0 || _restartTween != null)
                return;

            _playerInputMover.DeactivatePlayerControls();
            _playerInputShooter.DeactivatePlayerControls();

            _restartTween = DOTween.Sequence()
                .AppendInterval(_gameStaticData.HitInvulDuration)
                .AppendCallback(RespawnPlayer);
        }

        private void RespawnPlayer()
        {
            _restartTween = null;
            _playerInputMover.ActivatePlayerControls();
            _playerInputShooter.ActivatePlayerControls();
            _levelRestarter.LoadLevel();
        }
    }
}