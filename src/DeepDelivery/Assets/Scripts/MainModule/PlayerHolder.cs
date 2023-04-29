﻿using LevelWindowModule;
using UnityEngine;

namespace MainModule
{
    public class PlayerHolder
    {
        private readonly PlayerFactory _playerFactory;
        private readonly LevelHolder _levelHolder;
        public Player Player { get; private set; }

        public PlayerHolder(PlayerFactory playerFactory, LevelHolder levelHolder)
        {
            _playerFactory = playerFactory;
            _levelHolder = levelHolder;
        }
        
        public void CreatePlayer()
        {
            Player = _playerFactory.CreatePlayer();
            Vector3 position = _levelHolder.LevelModel.GetPlayerStartPosition();
            Player.SetPosition(position);
        }
    }
}