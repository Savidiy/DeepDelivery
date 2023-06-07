using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainModule
{
    public class RandomMover : IEnemyMover
    {
        private readonly EnemyBehaviour _enemyBehaviour;
        private readonly EnemySpawnPointBehaviour _enemySpawnPoint;
        private readonly EnemyStaticData _enemyStaticData;
        private int _targetIndex;

        public RandomMover(EnemyBehaviour enemyBehaviour, EnemySpawnPointBehaviour enemySpawnPoint,
            EnemyStaticData enemyStaticData)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyStaticData = enemyStaticData;
            SelectRandomTargetIndex();
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

                Vector3 target = GetTarget(pathPoints);

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
                    
                    currentPosition = target;
                    _enemyBehaviour.transform.position = currentPosition;
                    moveDistance -= distance;
                    SelectRandomTargetIndex();                   
                }
            }
        }

        public EnemyMoveProgress GetProgress() =>
            new(_targetIndex);

        public void LoadProgress(EnemyMoveProgress progress) =>
            _targetIndex = progress.TargetIndex;

        private Vector3 GetTarget(List<Transform> pathPoints)
        {
            return _targetIndex < 0
                ? _enemySpawnPoint.transform.position
                : pathPoints[_targetIndex].position;
        }

        private void UpdateLastMoveType(Vector3 moveTowards)
        {
            if (moveTowards.x > 0)
                LastMoveType = LastMoveType.ToRight;

            if (moveTowards.x < 0)
                LastMoveType = LastMoveType.ToLeft;
        }

        private void SelectRandomTargetIndex()
        {
            _targetIndex = Random.Range(-1, _enemySpawnPoint.PathPoints.Count);
        }
    }
}

