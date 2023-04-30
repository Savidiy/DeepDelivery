using UnityEngine;

namespace MainModule
{
    public class QuestTakeBehaviour : MonoBehaviour
    {
        public float InteractRadius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, InteractRadius);
        }
    }
}