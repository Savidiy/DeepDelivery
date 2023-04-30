using MvvmModule;

namespace MainModule
{
    public sealed class PlayerFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly ProgressProvider _progressProvider;
        private readonly PlayerInvulnerability _playerInvulnerability;

        public PlayerFactory(IPrefabFactory prefabFactory, ProgressProvider progressProvider,
            PlayerInvulnerability playerInvulnerability)
        {
            _prefabFactory = prefabFactory;
            _progressProvider = progressProvider;
            _playerInvulnerability = playerInvulnerability;
        }

        public Player CreatePlayer()
        {
            var playerBehaviour = _prefabFactory.Instantiate<PlayerBehaviour>("Player", parent: null);
            _playerInvulnerability.SetSpriteRenderers(playerBehaviour.BlinkSpriteRenderers);
            var player = new Player(playerBehaviour, _progressProvider, _playerInvulnerability);
            return player;
        }
    }
}