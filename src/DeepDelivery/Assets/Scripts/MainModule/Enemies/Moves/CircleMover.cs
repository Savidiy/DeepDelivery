using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    internal class CircleMover : IEnemyMover
    {
        private readonly EnemyBehaviour _enemyBehaviour;
        private readonly EnemySpawnPointBehaviour _enemySpawnPoint;
        private readonly EnemyStaticData _enemyStaticData;
        private int _targetIndex;

        public CircleMover(EnemyBehaviour enemyBehaviour, EnemySpawnPointBehaviour enemySpawnPoint,
            EnemyStaticData enemyStaticData)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyStaticData = enemyStaticData;
        }

        public void UpdatePosition(float deltaTime)
        {
            List<Transform> pathPoints = _enemySpawnPoint.PathPoints;
            if (pathPoints.Count == 0)
                return;

            float speed = _enemyStaticData.MoveSpeed;
            float moveDistance = speed * deltaTime;
            Vector3 currentPosition = _enemyBehaviour.transform.position;

            while (moveDistance > 0)
            {
                Vector3 target = GetTarget(pathPoints);
                float distance = Vector3.Distance(target, currentPosition);
                if (distance > moveDistance)
                {
                    Vector3 moveTowards = Vector3.MoveTowards(currentPosition, target, moveDistance);
                    _enemyBehaviour.transform.position = moveTowards;
                    moveDistance -= distance;
                }
                else
                {
                    currentPosition = target;
                    _enemyBehaviour.transform.position = currentPosition;
                    moveDistance -= distance;
                    _targetIndex++;
                    if (_targetIndex >= pathPoints.Count)
                        _targetIndex = -1;
                }
            }
        }

        private Vector3 GetTarget(List<Transform> pathPoints)
        {
            return _targetIndex < 0 
                ? _enemySpawnPoint.transform.position 
                : pathPoints[_targetIndex].position;
        }
    }
}