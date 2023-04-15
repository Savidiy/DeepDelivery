using System;
using System.Collections.Generic;
using Savidiy.Utils;
using UnityEngine;

namespace SettingsModule
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 0)]
    public class GameSettings : AutoSaveScriptableObject
    {
        public List<LevelData> Levels;
        public KeyCode[] RightKeys;
        public KeyCode[] LeftKeys;
        public KeyCode[] ShootKeys;
        public float DefaultMusicVolume = 0.3f;
        public float DefaultSoundVolume = 0.5f;
    }

    [Serializable]
    public class LevelData
    {
    }
}