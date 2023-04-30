﻿using System;
using Savidiy.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MainModule
{
    public class Enemy : IDisposable
    {
        private readonly EnemyBehaviour _enemyBehaviour;
        private readonly EnemyStaticData _enemyStaticData;

        public int Hp { get; private set; }
        public IEnemyMover EnemyMover { get; }

        public Enemy(EnemyBehaviour enemyBehaviour, EnemyStaticData enemyStaticData, IEnemyMover enemyMover)
        {
            EnemyMover = enemyMover;
            _enemyStaticData = enemyStaticData;
            _enemyBehaviour = enemyBehaviour;
            Hp = enemyStaticData.HealthPoints;
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
        }

        private void UpdateName()
        {
            _enemyBehaviour.name = $"{_enemyStaticData.EnemyType} HP={Hp}";
        }

        public void Clear()
        {
            
        }
    }

    public interface IEnemyMover
    {
        void UpdatePosition(float deltaTime);
    }
}