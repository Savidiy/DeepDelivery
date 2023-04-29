using UnityEngine;

namespace MainModule
{
    public class GunBehaviour : MonoBehaviour
    {
        public Vector3 ShootDirection;
        public Transform BulletSpawnPoint;

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}