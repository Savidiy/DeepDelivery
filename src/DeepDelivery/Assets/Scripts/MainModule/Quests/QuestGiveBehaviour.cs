using UnityEngine;

namespace MainModule
{
    public class QuestGiveBehaviour : MonoBehaviour
    {
        public QuestTakeBehaviour QuestTakeBehaviour;
        public float InteractRadius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, InteractRadius);

            if (QuestTakeBehaviour != null)
                Gizmos.DrawLine(transform.position, QuestTakeBehaviour.transform.position);
        }
    }
}