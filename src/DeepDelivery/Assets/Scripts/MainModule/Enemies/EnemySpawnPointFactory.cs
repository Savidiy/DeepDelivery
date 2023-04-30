namespace MainModule
{
    public class EnemySpawnPointFactory
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly GameStaticData _gameStaticData;
        private readonly ICameraProvider _cameraProvider;

        public EnemySpawnPointFactory(EnemyFactory enemyFactory, GameStaticData gameStaticData, ICameraProvider cameraProvider)
        {
            _enemyFactory = enemyFactory;
            _gameStaticData = gameStaticData;
            _cameraProvider = cameraProvider;
        }

        public EnemySpawnPoint Create(EnemySpawnPointBehaviour enemySpawnPointBehaviour)
        {
            return new EnemySpawnPoint(enemySpawnPointBehaviour, _enemyFactory, _gameStaticData, _cameraProvider);
        }
    }
}