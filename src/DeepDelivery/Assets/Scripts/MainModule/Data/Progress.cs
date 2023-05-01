using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainModule
{
    [Serializable]
    public class Progress
    {
        public int LevelIndex = 0;
        public int MaxHp;
        public int CurrentHp;
        public List<GunType> ActiveGuns;

        public string LastActiveCheckPointId = string.Empty;
        public PlayerProgress Player = new();

        public Progress(StartProgress progress)
        {
            LevelIndex = progress.LevelIndex;
            MaxHp = progress.MaxHp;
            CurrentHp = MaxHp;
            ActiveGuns = new List<GunType>(progress.ActiveGuns);
        }
    }

    [Serializable]
    public class PlayerProgress
    {
        public bool HasSavedPosition;
        public SerializableVector3 SavedPosition;
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