using System.Collections.Generic;

namespace MainModule
{
    public class EnemySpawnPointFactory
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly CameraProvider _cameraProvider;
        private readonly EnemyStaticDataProvider _enemyStaticDataProvider;

        public EnemySpawnPointFactory(EnemyFactory enemyFactory, CameraProvider cameraProvider,
            EnemyStaticDataProvider enemyStaticDataProvider)
        {
            _enemyFactory = enemyFactory;
            _cameraProvider = cameraProvider;
            _enemyStaticDataProvider = enemyStaticDataProvider;
        }

        public List<EnemySpawnPoint> CreatePoints(List<EnemySpawnPointBehaviour> behaviours)
        {
            var enemySpawnPoints = new List<EnemySpawnPoint>();
            foreach (EnemySpawnPointBehaviour behaviour in behaviours)
                enemySpawnPoints.Add(Create(behaviour));

            return enemySpawnPoints;
        }

        private EnemySpawnPoint Create(EnemySpawnPointBehaviour enemySpawnPointBehaviour)
        {
            return new EnemySpawnPoint(enemySpawnPointBehaviour, _enemyFactory, _cameraProvider, _enemyStaticDataProvider);
        }
    }
}