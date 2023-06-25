using UnityEngine;

namespace MainModule
{
    public class PlayerHolder : IProgressWriter
    {
        private readonly LevelHolder _levelHolder;
        public PlayerVisual PlayerVisual { get; }

        public PlayerHolder(PlayerFactory playerFactory, LevelHolder levelHolder, ProgressUpdater progressUpdater)
        {
            _levelHolder = levelHolder;
            progressUpdater.Register(this);

            PlayerVisual = playerFactory.CreatePlayer();
        }

        public void LoadProgress(Progress progress)
        {
            Vector3 defaultPosition = _levelHolder.LevelModel.GetPlayerStartPosition();
            PlayerVisual.LoadProgress(progress.Player, defaultPosition);
        }

        public void UpdateProgress(Progress progress)
        {
            PlayerVisual.UpdateProgress(progress.Player);
        }
    }
}