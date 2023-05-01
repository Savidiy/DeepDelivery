using UnityEngine;

namespace MainModule
{
    public class PlayerHolder : IProgressWriter
    {
        private readonly LevelHolder _levelHolder;
        public Player Player { get; }

        public PlayerHolder(PlayerFactory playerFactory, LevelHolder levelHolder, ProgressUpdater progressUpdater)
        {
            _levelHolder = levelHolder;
            progressUpdater.Register(this);

            Player = playerFactory.CreatePlayer();
        }

        public void LoadProgress(Progress progress)
        {
            Vector3 defaultPosition = _levelHolder.LevelModel.GetPlayerStartPosition();
            Player.LoadProgress(progress.Player, defaultPosition);
        }

        public void UpdateProgress(Progress progress)
        {
            Player.UpdateProgress(progress.Player);
        }
    }
}