using UnityEngine;

namespace MainModule
{
    public class PlayerHolder : IProgressWriter
    {
        private readonly PlayerFactory _playerFactory;
        private readonly LevelHolder _levelHolder;
        public Player Player { get; private set; }

        public PlayerHolder(PlayerFactory playerFactory, LevelHolder levelHolder, ProgressUpdater progressUpdater)
        {
            Debug.Log($"{GetType()} ctor");
            _playerFactory = playerFactory;
            _levelHolder = levelHolder;
            progressUpdater.Register(this);
        }

        public void LoadProgress(Progress progress)
        {            
            Debug.Log($"{GetType()} LoadProgress");

            Player?.Dispose();

            Player = _playerFactory.CreatePlayer();

            Vector3 position = progress.Player.HasSavedPosition
                ? progress.Player.SavedPosition.ToVector3()
                : _levelHolder.LevelModel.GetPlayerStartPosition();

            Player.SetPosition(position);
        }

        public void UpdateProgress(Progress progress)
        {
            progress.Player.HasSavedPosition = true;
            progress.Player.SavedPosition = new SerializableVector3(Player.Position);
        }
    }
}