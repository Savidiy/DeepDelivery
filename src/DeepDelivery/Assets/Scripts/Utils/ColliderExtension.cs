using UnityEngine;

namespace Savidiy.Utils
{
    public static class ColliderExtension
    {
        public static bool HasCollisionWith(this Collider2D collider, Collider2D anotherCollider)
        {
            ColliderDistance2D distance2D = Physics2D.Distance(collider, anotherCollider);
            return distance2D.isOverlapped;
        }
    }
}