using System;
using System.Collections.Generic;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    [Serializable]
    public class Progress
    {
        public int LevelIndex;

        public string LastActiveCheckPointId = string.Empty;
        public PlayerProgress Player = new();
        public List<string> CollectedItemId = new();
        public List<string> TookQuestsId = new();
        public List<string> CompletedQuestsId = new();
        public List<string> SoldOutShopsId = new();
        public List<EnemySpawnPointProgress> EnemySpawnPoints = new();

        public Progress(StartProgress startProgress)
        {
            LevelIndex = startProgress.LevelIndex;
            Player.MaxHp = startProgress.MaxHp;
            Player.CurrentHp = Player.MaxHp;
            Player.ActiveGuns = new List<GunType>(startProgress.ActiveGuns);
        }
    }

    [Serializable]
    public class EnemySpawnPointProgress
    {
        public string Id;
        public bool HasEnemy;
        public bool EnemyWasCreated;
        public float Timer;
        public SerializableVector3 EnemyPosition;
        public SerializableVector3 EnemyRotation;
        public int EnemyHp;
        public EnemyMoveProgress EnemyMoveProgress;

        public EnemySpawnPointProgress()
        {
        }

        public EnemySpawnPointProgress(string id, bool hasEnemy, bool enemyWasCreated, float timer, Vector3 enemyPosition,
            Quaternion enemyRotation, int enemyHp, EnemyMoveProgress enemyMoveProgress)
        {
            Id = id;
            HasEnemy = hasEnemy;
            EnemyWasCreated = enemyWasCreated;
            Timer = timer;
            EnemyPosition = new SerializableVector3(enemyPosition);
            EnemyRotation = new SerializableVector3(enemyRotation.eulerAngles);
            EnemyHp = enemyHp;
            EnemyMoveProgress = enemyMoveProgress;
        }
    }

    [Serializable]
    public class EnemyMoveProgress
    {
        public int TargetIndex;
        public bool IsBackward;
        public float Timer;

        public EnemyMoveProgress(int targetIndex = 0, bool isBackward = false, float timer = 0f)
        {
            TargetIndex = targetIndex;
            IsBackward = isBackward;
            Timer = timer;
        }
    }

    [Serializable]
    public class PlayerProgress
    {
        public int MaxHp;
        public int CurrentHp;
        public List<GunType> ActiveGuns = new();
        public bool HasSavedPosition;
        public SerializableVector3 SavedPosition;
        public List<ItemType> Items = new();
    }

    [Serializable]
    public class SerializableVector3
    {
        public float X;
        public float Y;
        public float Z;

        public SerializableVector3(Vector3 vector3)
        {
            X = vector3.x;
            Y = vector3.y;
            Z = vector3.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
        }
    }
}