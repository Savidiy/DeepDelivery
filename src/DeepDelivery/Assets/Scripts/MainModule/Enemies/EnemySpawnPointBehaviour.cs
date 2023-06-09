﻿#if UNITY_EDITOR
#endif
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using static MainModule.RespawnType;

namespace MainModule
{
    public class EnemySpawnPointBehaviour : MonoBehaviour
    {
        public EnemyType EnemyType = EnemyType.Fish;
        public RespawnType RespawnType = ByTimerWhenInvisible;

        private bool ShowUseCustomTimerDuration => RespawnType is ByTimer or ByTimerWhenInvisible;
        [ShowIf(nameof(ShowUseCustomTimerDuration))] public float StartTimerValue;
        [ShowIf(nameof(ShowUseCustomTimerDuration))] public bool UseCustomTimerDuration;
        private bool ShowCustomTimerDuration => RespawnType is ByTimer or ByTimerWhenInvisible && UseCustomTimerDuration;
        [ShowIf(nameof(ShowCustomTimerDuration))] public float CustomTimerDuration;

        [ShowIf(nameof(ShowUseCustomTimerDuration)), ReadOnly, NonSerialized, ShowInInspector] public float Timer;

        public MoveType MoveType;
        private bool NeedShowPathPoints => MoveType != MoveType.None;
        [ShowIf(nameof(NeedShowPathPoints))] public List<Transform> PathPoints = new();

        public UniqueId UniqueId;

        public void SetTimerInfo(float timer)
        {
            Timer = timer;
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        private void OnValidate()
        {
            name = $"Enemy Spawn - {EnemyType}";

            if (RespawnType == ByTimer)
                name += UseCustomTimerDuration
                    ? $" - T:{CustomTimerDuration}s"
                    : " - T:d";

            if (RespawnType == ByTimerWhenInvisible)
                name += UseCustomTimerDuration
                    ? $" - T:inv,{CustomTimerDuration}s"
                    : " - T:inv,d";

            if (StartTimerValue > 0)
                name += $" - S:{StartTimerValue}s";

            name += MoveType switch
            {
                MoveType.None => "",
                MoveType.Circle => " - M:C",
                MoveType.PingPong => " - M:PP",
                MoveType.Teleport => " - M:T",
                MoveType.Random => " - M:R",
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

            SelectColor();

            Vector3 from = transform.position;

            if (MoveType == MoveType.Random)
            {
                DrawMoveRandomPath(from);
            }
            else
            {
                DrawMovePingPongAndCirclePath(from);
            }
        }

        private void SelectColor ()
        {
            Gizmos.color = MoveType switch
            {
                MoveType.Circle => Color.yellow,
                MoveType.PingPong => Color.white,
                MoveType.Teleport => Color.cyan,
                MoveType.Random => Color.red,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void DrawMoveRandomPath(Vector3 from)
        {
            for (int i = 0; i< PathPoints.Count; i++)
            {
                Gizmos.DrawLine(from, PathPoints[i].position);

                if (PathPoints[i] == null || PathPoints[i] == gameObject.transform)
                    continue;
                
                for (int j = 0; j < PathPoints.Count; j++)
                {
                    if (i < j) Gizmos.DrawLine(PathPoints[i].position, PathPoints[j].position);
                }               
            }
        }

        private void DrawMovePingPongAndCirclePath(Vector3 from)
        {
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