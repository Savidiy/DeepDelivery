namespace MainModule
{
    public class EnemySpawnPointFactory : IFactory<EnemySpawnPoint, EnemySpawnPointBehaviour>
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly ICameraProvider _cameraProvider;
        private readonly EnemyStaticDataProvider _enemyStaticDataProvider;

        public EnemySpawnPointFactory(EnemyFactory enemyFactory, ICameraProvider cameraProvider, EnemyStaticDataProvider enemyStaticDataProvider)
        {
            _enemyFactory = enemyFactory;
            _cameraProvider = cameraProvider;
            _enemyStaticDataProvider = enemyStaticDataProvider;
        }

        public EnemySpawnPoint Create(EnemySpawnPointBehaviour enemySpawnPointBehaviour)
        {
            return new EnemySpawnPoint(enemySpawnPointBehaviour, _enemyFactory, _cameraProvider, _enemyStaticDataProvider);
        }
    }
}