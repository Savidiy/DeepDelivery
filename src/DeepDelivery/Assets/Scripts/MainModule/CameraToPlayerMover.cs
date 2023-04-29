using LevelWindowModule.Contracts;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    public class CameraToPlayerMover
    {
        private readonly TickInvoker _tickInvoker;
        private readonly PlayerHolder _playerHolder;
        private readonly ICameraProvider _cameraProvider;

        public CameraToPlayerMover(TickInvoker tickInvoker, PlayerHolder playerHolder, ICameraProvider cameraProvider)
        {
            _tickInvoker = tickInvoker;
            _playerHolder = playerHolder;
            _cameraProvider = cameraProvider;
        }

        public void Activate()
        {
            _tickInvoker.Updated -= OnUpdated;
            _tickInvoker.Updated += OnUpdated;
            OnUpdated();
        }

        public void Deactivate()
        {
            _tickInvoker.Updated -= OnUpdated;
        }

        private void OnUpdated()
        {
            Player player = _playerHolder.Player;
            Vector3 playerPosition = player.Position;

            Transform cameraTransform = _cameraProvider.Camera.transform;
            Vector3 cameraPosition = cameraTransform.position;
            cameraPosition.x = playerPosition.x;
            cameraPosition.y = playerPosition.y;

            cameraTransform.position = cameraPosition;
        }
    }
}