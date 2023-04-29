using MvvmModule;
using UnityEngine;

namespace MainModule
{
    public class BulletFactory
    {
        private const string BULLET = "Bullet";
        
        private readonly IPrefabFactory _prefabFactory;
        private readonly Transform _root;

        public BulletFactory(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;

            _root = new GameObject("BulletRoot").transform;
        }
        
        public Bullet CreateBullet(Vector3 gunPosition, Vector3 gunDirection, bool isPlayerBullet)
        {
            var bulletBehaviour = _prefabFactory.Instantiate<BulletBehaviour>(BULLET, _root);
            bulletBehaviour.transform.position = gunPosition;

            return new Bullet(bulletBehaviour, gunDirection, isPlayerBullet);
        }
    }
}