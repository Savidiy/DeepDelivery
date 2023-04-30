using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MainModule
{
    [CreateAssetMenu(fileName = "GameStaticData", menuName = "GameStaticData", order = 0)]
    public class GameStaticData : AutoSaveScriptableObject
    {
        private const string INPUT = "Input";
        private const string SOUND = "Sound";
        public List<LevelData> Levels;

        [FoldoutGroup(INPUT)] public KeyCode[] RightKeys;
        [FoldoutGroup(INPUT)] public KeyCode[] LeftKeys;
        [FoldoutGroup(INPUT)] public KeyCode[] UpKeys;
        [FoldoutGroup(INPUT)] public KeyCode[] DownKeys;
        [FoldoutGroup(INPUT)] public KeyCode[] ShootKeys;

        public float PlayerSpeedX = 1;
        public float PlayerSpeedY = 1;
        
        [FoldoutGroup(SOUND)] public float DefaultMusicVolume = 0.3f;
        [FoldoutGroup(SOUND)] public float DefaultSoundVolume = 0.5f;

        public Progress StartProgress;
        public float HitInvulDuration = 2;
        public float BlinkPeriod = 0.2f;
        public float ShootCooldown = 0.5f;
        public float PlayerBulletSpeed = 8f;
        public float CompassDrawDistance = 1f;
    }

    [Serializable]
    public class LevelData
    {
        public LevelBehaviour LevelBehaviour;
    }
}