using UnityEngine;

namespace MainModule
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public Transform FlipRoot;
        public Rigidbody2D Rigidbody;
        public Collider2D Collider2D;
        public SpriteRenderer[] BlinkSpriteRenderers;
        public GunBehaviour TopGun;
        public GunBehaviour BottomGun;
        public GunBehaviour ForwardGun;
    }
}