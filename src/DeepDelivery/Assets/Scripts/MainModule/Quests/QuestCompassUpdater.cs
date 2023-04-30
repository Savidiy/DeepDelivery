using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class QuestCompassUpdater
    {
        private const string PREFAB_NAME = "Compass";

        private readonly IPrefabFactory _prefabFactory;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly Transform _root;
        private readonly List<CompassBehaviour> _compasses = new();
        private readonly GameStaticData _gameStaticData;

        public QuestCompassUpdater(TickInvoker tickInvoker, PlayerHolder playerHolder, IPrefabFactory prefabFactory,
            GameStaticData gameStaticData)
        {
            _gameStaticData = gameStaticData;
            _prefabFactory = prefabFactory;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;

            _root = new GameObject("CompassRoot").transform;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            UpdateCompassCount();
            UpdateCompassesPosition();
        }

        private void UpdateCompassCount()
        {
            Player player = _playerHolder.Player;
            List<Quest> quests = player.Quests;
            int count = quests.Count;

            for (int i = _compasses.Count - 1; i >= count; i--)
            {
                Object.Destroy(_compasses[i].gameObject);
                _compasses.RemoveAt(i);
            }

            for (int i = _compasses.Count; i < count; i++)
            {
                var compassBehaviour = _prefabFactory.Instantiate<CompassBehaviour>(PREFAB_NAME, _root);
                _compasses.Add(compassBehaviour);
            }
        }

        private void UpdateCompassesPosition()
        {
            Player player = _playerHolder.Player;
            Vector3 playerPosition = player.Position;
            List<Quest> quests = player.Quests;

            for (var index = 0; index < quests.Count; index++)
            {
                UpdateCompass(quests, index, playerPosition);
            }
        }

        private void UpdateCompass(List<Quest> quests, int index, Vector3 playerPosition)
        {
            Quest quest = quests[index];
            Vector3 targetPosition = quest.GetTargetPosition();
            targetPosition.z = playerPosition.z;
            Vector3 forward = targetPosition - playerPosition;
            forward.Normalize();

            float compassDrawDistance = _gameStaticData.CompassDrawDistance;
            Vector3 shift = forward * compassDrawDistance;

            Vector3 position = playerPosition + shift;

            CompassBehaviour compassBehaviour = _compasses[index];
            Transform transform = compassBehaviour.transform;
            transform.position = position;
            transform.LookAt(targetPosition);
        }
    }
}