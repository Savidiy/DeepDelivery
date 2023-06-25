using MvvmModule;
using Zenject;

namespace MainModule
{
    public sealed class PlayerFactory
    {
        private readonly IPrefabFactory _prefabFactory;
        private readonly PlayerInvulnerability _playerInvulnerability;
        private readonly IInstantiator _instantiator;

        public PlayerFactory(IPrefabFactory prefabFactory, PlayerInvulnerability playerInvulnerability, IInstantiator instantiator)
        {
            _prefabFactory = prefabFactory;
            _playerInvulnerability = playerInvulnerability;
            _instantiator = instantiator;
        }

        public PlayerVisual CreatePlayer()
        {
            var playerBehaviour = _prefabFactory.Instantiate<PlayerBehaviour>("Player", parent: null);
            _playerInvulnerability.SetSpriteRenderers(playerBehaviour.BlinkSpriteRenderers);
            var player = _instantiator.Instantiate<PlayerVisual>(new object[] {playerBehaviour});
            return player;
        }
    }
}