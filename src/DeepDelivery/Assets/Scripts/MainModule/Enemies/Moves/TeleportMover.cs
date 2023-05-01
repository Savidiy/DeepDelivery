using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    internal class TeleportMover : IEnemyMover
    {
        private readonly EnemyBehaviour _enemyBehaviour;
        private readonly EnemySpawnPointBehaviour _enemySpawnPoint;
        private readonly EnemyStaticData _enemyStaticData;
        private int _targetIndex;

        public TeleportMover(EnemyBehaviour enemyBehaviour, EnemySpawnPointBehaviour enemySpawnPoint,
            EnemyStaticData enemyStaticData)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyStaticData = enemyStaticData;
        }

        public LastMoveType LastMoveType { get; private set; }

        public void UpdatePosition(float deltaTime)
        {
            LastMoveType = LastMoveType.None;
            List<Transform> pathPoints = _enemySpawnPoint.PathPoints;
            if (pathPoints.Count == 0)
                return;

            float speed = _enemyStaticData.MoveSpeed;
            float moveDistance = speed * deltaTime;
            Vector3 currentPosition = _enemyBehaviour.transform.position;

            while (moveDistance > 0)
            {
                Vector3 target = pathPoints[_targetIndex].position;
                
                UpdateLastMoveType(target - currentPosition);
                
                float distance = Vector3.Distance(target, currentPosition);
                if (distance > moveDistance)
                {
                    Vector3 moveTowards = Vector3.MoveTowards(currentPosition, target, moveDistance);
                    _enemyBehaviour.transform.position = moveTowards;
                    moveDistance -= distance;
                }
                else
                {
                    _enemyBehaviour.transform.position = target;
                    moveDistance -= distance;
                    _targetIndex++;
                    if (_targetIndex >= pathPoints.Count)
                    {
                        _targetIndex = 0;
                        currentPosition = _enemySpawnPoint.transform.position;
                        _enemyBehaviour.transform.position = currentPosition;
                    }
                }
            }
        }

        public EnemyMoveProgress GetProgress() => new(_targetIndex);

        public void LoadProgress(EnemyMoveProgress progress) => _targetIndex = progress.TargetIndex;
   
        private void UpdateLastMoveType(Vector3 moveTowards)
        {
            if (moveTowards.x > 0)
                LastMoveType = LastMoveType.ToRight;

            if (moveTowards.x < 0)
                LastMoveType = LastMoveType.ToLeft;
        } }
}