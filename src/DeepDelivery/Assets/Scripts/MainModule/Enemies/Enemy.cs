using System;
using Savidiy.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Enemy : IDisposable
    {
        private readonly EnemyBehaviour _enemyBehaviour;
        private readonly EnemyStaticData _enemyStaticData;
        private readonly EnemyBlinkSettings _enemyBlinkSettings;

        public int Hp { get; private set; }
        public IEnemyMover EnemyMover { get; }
        public Vector3 Position => _enemyBehaviour.transform.position;
        public Quaternion Rotation => _enemyBehaviour.transform.rotation;
        public EnemyType EnemyType { get; }

        public Enemy(EnemyBehaviour enemyBehaviour, EnemyStaticData enemyStaticData, IEnemyMover enemyMover,
            EnemyBlinkSettings enemyBlinkSettings)
        {
            EnemyMover = enemyMover;
            _enemyStaticData = enemyStaticData;
            _enemyBlinkSettings = enemyBlinkSettings;
            _enemyBehaviour = enemyBehaviour;
            Hp = enemyStaticData.HealthPoints;
            EnemyType = enemyStaticData.EnemyType;
            UpdateName();
        }

        public bool HasCollisionWith(Collider2D anotherCollider)
        {
            return _enemyBehaviour.HitCollider.HasCollisionWith(anotherCollider);
        }

        public void Dispose()
        {
            Object.Destroy(_enemyBehaviour.gameObject);
        }

        public void GetHit()
        {
            Hp--;
            UpdateName();
            _enemyBehaviour.Flash(_enemyBlinkSettings);
        }

        private void UpdateName()
        {
            _enemyBehaviour.name = $"{_enemyStaticData.EnemyType} HP={Hp}";
        }

        public void LoadProgress(EnemySpawnPointProgress progress)
        {
            Hp = progress.EnemyHp;
            _enemyBehaviour.transform.position = progress.EnemyPosition.ToVector3();
            _enemyBehaviour.transform.rotation = Quaternion.Euler(progress.EnemyRotation.ToVector3());
            EnemyMover.LoadProgress(progress.EnemyMoveProgress);
        }

        public void Move(float deltaTime)
        {
            EnemyMover.UpdatePosition(deltaTime);
            LastMoveType lastMoveType = EnemyMover.LastMoveType;
            _enemyBehaviour.UpdateHeadDirection(lastMoveType);
        }
    }
}