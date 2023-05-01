using UnityEngine;

namespace MainModule
{
    public class CheckPointBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject UsedGroup;
        [SerializeField] private GameObject UnusedGroup;
        public Collider2D Collider;
        public UniqueId UniqueId;

        public void MarkUsed()
        {
            UsedGroup.SetActive(true);
            UnusedGroup.SetActive(false);
        }

        public void MarkUnused()
        {
            UsedGroup.SetActive(false);
            UnusedGroup.SetActive(true);
        }
    }
}