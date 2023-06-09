﻿using System.Collections.Generic;
using MvvmModule;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class QuestCompassUpdater
    {
        private const string PREFAB_NAME = "QuestCompass";

        private readonly IPrefabFactory _prefabFactory;
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly Transform _root;
        private readonly List<QuestCompassBehaviour> _compasses = new();
        private readonly GameStaticData _gameStaticData;
        private readonly PlayerQuestsHandler _playerQuestsHandler;

        public QuestCompassUpdater(TickInvoker tickInvoker, PlayerHolder playerHolder, IPrefabFactory prefabFactory,
            GameStaticData gameStaticData, PlayerQuestsHandler playerQuestsHandler)
        {
            _gameStaticData = gameStaticData;
            _playerQuestsHandler = playerQuestsHandler;
            _prefabFactory = prefabFactory;
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;

            _root = new GameObject("QuestCompassRoot").transform;
        }

        public void Activate()
        {
            _tickInvoker.Subscribe(UpdateType.Update, OnUpdated);
        }

        public void Deactivate()
        {
            _tickInvoker.Unsubscribe(UpdateType.Update, OnUpdated);
        }

        private void OnUpdated()
        {
            UpdateCompassCount();
            UpdateCompassesPosition();
        }

        private void UpdateCompassCount()
        {
            List<Quest> quests = _playerQuestsHandler.Quests;
            int count = quests.Count;

            for (int i = _compasses.Count - 1; i >= count; i--)
            {
                Object.Destroy(_compasses[i].gameObject);
                _compasses.RemoveAt(i);
            }

            for (int i = _compasses.Count; i < count; i++)
            {
                var compassBehaviour = _prefabFactory.Instantiate<QuestCompassBehaviour>(PREFAB_NAME, _root);
                _compasses.Add(compassBehaviour);
            }
        }

        private void UpdateCompassesPosition()
        {
            PlayerVisual playerVisual = _playerHolder.PlayerVisual;
            Vector3 playerPosition = playerVisual.Position;
            List<Quest> quests = _playerQuestsHandler.Quests;

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

            float compassDrawDistance = _gameStaticData.QuestCompassDrawDistance;
            Vector3 shift = forward * compassDrawDistance;

            Vector3 position = playerPosition + shift;

            QuestCompassBehaviour questCompassBehaviour = _compasses[index];
            Transform transform = questCompassBehaviour.transform;
            transform.position = position;
            transform.LookAt(targetPosition);
        }
    }
}