using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    internal class PingPongMover : IEnemyMover
    {
        private readonly EnemyBehaviour _enemyBehaviour;
        private readonly EnemySpawnPointBehaviour _enemySpawnPoint;
        private readonly EnemyStaticData _enemyStaticData;
        private int _targetIndex;
        private bool _isBackward;
        private float _delayTimer;

        public LastMoveType LastMoveType { get; private set; }

        public PingPongMover(EnemyBehaviour enemyBehaviour, EnemySpawnPointBehaviour enemySpawnPoint,
            EnemyStaticData enemyStaticData)
        {
            _enemyBehaviour = enemyBehaviour;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyStaticData = enemyStaticData;
        }

        public void UpdatePosition(float deltaTime)
        {
            LastMoveType = LastMoveType.None;
            List<Transform> pathPoints = _enemySpawnPoint.PathPoints;
            if (pathPoints.Count == 0)
                return;

            if (_delayTimer > 0)
            {
                _delayTimer -= deltaTime;
                return;
            }

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
                    SelectNextTargetIndex(pathPoints);
                }
            }
        }

        private void UpdateLastMoveType(Vector3 shift)
        {
            if (shift.x > 0)
                LastMoveType = LastMoveType.ToRight;

            if (shift.x < 0)
                LastMoveType = LastMoveType.ToLeft;
        }

        public EnemyMoveProgress GetProgress()
        {
            return new EnemyMoveProgress(_targetIndex, _isBackward, _delayTimer);
        }

        public void LoadProgress(EnemyMoveProgress progress)
        {
            _targetIndex = progress.TargetIndex;
            _isBackward = progress.IsBackward;
            _delayTimer = progress.Timer;
        }

        private void SelectNextTargetIndex(List<Transform> pathPoints)
        {
            if (_isBackward)
                _targetIndex--;
            else
                _targetIndex++;

            if (_targetIndex >= pathPoints.Count)
            {
                _targetIndex = pathPoints.Count - 2;
                _isBackward = true;
                _delayTimer += _enemyStaticData.PingPongReverseDelay;
            }

            if (_targetIndex < -1)
            {
                _targetIndex = 0;
                _isBackward = false;
                _delayTimer += _enemyStaticData.PingPongReverseDelay;
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