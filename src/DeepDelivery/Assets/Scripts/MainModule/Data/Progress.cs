using System;
using System.Collections.Generic;
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

        public Progress(StartProgress startProgress)
        {
            LevelIndex = startProgress.LevelIndex;
            Player.MaxHp = startProgress.MaxHp;
            Player.CurrentHp = Player.MaxHp;
            Player.ActiveGuns = new List<GunType>(startProgress.ActiveGuns);
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