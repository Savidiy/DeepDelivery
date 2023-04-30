using System;
using System.Collections.Generic;
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

        public MoveType MoveType;
        private bool NeedShowPathPoints => MoveType != MoveType.None;
        [ShowIf(nameof(NeedShowPathPoints))] public List<Transform> PathPoints = new();

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
                    ? $" - T:{CustomTimerDuration}s"
                    : " - T:d";

            if (RespawnType == ByTimerWhenInvisible)
                name += UseCustomTimerDuration
                    ? $" - T:inv,{CustomTimerDuration}s"
                    : " - T:inv,d";

            name += MoveType switch
            {
                MoveType.None => "",
                MoveType.Circle => " - M:C",
                MoveType.PingPong => " - M:PP",
                MoveType.Teleport => " - M:T",
                _ => throw new ArgumentOutOfRangeException()
            };

            PathPoints.Clear();
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = transform.GetChild(i);
                child.gameObject.name = $"PathPoint {i + 1}";
                PathPoints.Add(child);
            }
        }

        private void OnDrawGizmos()
        {
            DrawMovePath();
        }

        private void DrawMovePath()
        {
            if (MoveType == MoveType.None)
                return;

            Gizmos.color = MoveType switch
            {
                MoveType.Circle => Color.yellow,
                MoveType.PingPong => Color.white,
                MoveType.Teleport => Color.cyan,
                _ => throw new ArgumentOutOfRangeException()
            };

            Vector3 from = transform.position;
            foreach (Transform pathPoint in PathPoints)
            {
                if (pathPoint == null)
                    continue;
                
                Vector3 to = pathPoint.position;
                Gizmos.DrawLine(from, to);
                from = to;
            }

            if (MoveType == MoveType.Circle)
            {
                Vector3 to = transform.position;
                Gizmos.DrawLine(from, to);
            }
        }
    }
}