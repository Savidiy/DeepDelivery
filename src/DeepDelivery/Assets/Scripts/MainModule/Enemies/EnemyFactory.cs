using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class EnemyFactory
    {
        private readonly EnemyStaticDataProvider _enemyStaticDataProvider;
        private readonly Transform _root;

        public EnemyFactory(EnemyStaticDataProvider enemyStaticDataProvider)
        {
            _enemyStaticDataProvider = enemyStaticDataProvider;
            _root = new GameObject("EnemyRoot").transform;
        }

        public Enemy Create(EnemySpawnPointBehaviour enemySpawnPoint)
        {
            EnemyType enemyType = enemySpawnPoint.EnemyType;
            EnemyStaticData enemyData = _enemyStaticDataProvider.GetEnemyData(enemyType);
            EnemyBehaviour enemyBehaviour = Object.Instantiate(enemyData.EnemyBehaviour, _root);
            enemyBehaviour.transform.position = enemySpawnPoint.transform.position;
            enemyBehaviour.transform.rotation = enemySpawnPoint.transform.rotation;
            EnemyBlinkSettings enemyBlinkSettings = _enemyStaticDataProvider.EnemyBlinkSettings;

            IEnemyMover mover = CreateMover(enemyBehaviour, enemySpawnPoint, enemyData);
            return new Enemy(enemyBehaviour, enemyData, mover, enemyBlinkSettings);
        }

        private IEnemyMover CreateMover(EnemyBehaviour enemyBehaviour, EnemySpawnPointBehaviour enemySpawnPoint,
            EnemyStaticData enemyStaticData)
        {
            return enemySpawnPoint.MoveType switch
            {
                MoveType.None => new NoneMover(),
                MoveType.Circle => new CircleMover(enemyBehaviour, enemySpawnPoint, enemyStaticData),
                MoveType.PingPong => new PingPongMover(enemyBehaviour, enemySpawnPoint, enemyStaticData),
                MoveType.Teleport => new TeleportMover(enemyBehaviour, enemySpawnPoint, enemyStaticData),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}