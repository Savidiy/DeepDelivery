using System;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using static SettingsModule.RespawnType;

namespace SettingsModule
{
    public class EnemySpawnPointBehaviour : MonoBehaviour
    {
        public EnemyType EnemyType = EnemyType.Fish;
        public RespawnType RespawnType = ByTimerWhenInvisible;

        private bool ShowUseCustomTimerDuration => RespawnType is ByTimer or ByTimerWhenInvisible;
        [ShowIf(nameof(ShowUseCustomTimerDuration))] public bool UseCustomTimerDuration;
        private bool ShowCustomTimerDuration => RespawnType is ByTimer or ByTimerWhenInvisible && UseCustomTimerDuration;
        [ShowIf(nameof(ShowCustomTimerDuration))] public float CustomTimerDuration;

        [ShowIf(nameof(ShowUseCustomTimerDuration)), ReadOnly, NonSerialized, ShowInInspector] public float Timer;

        public void SetTimerInfo(float timer)
        {
            Timer = timer;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private void OnValidate()
        {
            name = $"SpawnPoint - {EnemyType}";

            if (RespawnType == ByTimer)
                name += UseCustomTimerDuration
                    ? $" (timer {CustomTimerDuration}s)"
                    : " (default timer)";

            if (RespawnType == ByTimerWhenInvisible)
                name += UseCustomTimerDuration
                    ? $" (invis timer {CustomTimerDuration}s)"
                    : " (invis default Timer)";
        }
    }
}