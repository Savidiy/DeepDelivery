using System;
using System.Collections.Generic;
using Savidiy.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MainModule
{
    [CreateAssetMenu(fileName = nameof(GameStaticData), menuName = nameof(GameStaticData), order = 0)]
    public class GameStaticData : AutoSaveScriptableObject
    {
        private const string SOUND = "Sound";
        public List<LevelData> Levels;

        [FoldoutGroup(SOUND)] public float DefaultMusicVolume = 0.3f;
        [FoldoutGroup(SOUND)] public float DefaultSoundVolume = 0.5f;

        public StartProgress StartProgress;
        public bool UseDebugProgress;
        public StartProgress DebugStartProgress;
        public float HitInvulDuration = 2;
        public float BlinkPeriod = 0.2f;
        public float HurtPlayerDelay = 0.06f;
        public float ShootCooldown = 0.5f;
        public float PlayerSpeedX = 1;
        public float PlayerSpeedY = 1;
        public float PlayerBulletSpeed = 8f;
        public float QuestCompassDrawDistance = 3.21f;
        public float ItemCompassDrawDistance = 2.5f;
    }
    
    [Serializable]
    public class StartProgress
    {
        public int LevelIndex = 0;
        public int MaxHp;
        public List<GunType> ActiveGuns;
    }

    [Serializable]
    public class LevelData
    {
        public LevelBehaviour LevelBehaviour;
    }
}