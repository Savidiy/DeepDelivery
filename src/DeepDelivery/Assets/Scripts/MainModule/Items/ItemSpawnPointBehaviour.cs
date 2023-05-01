using UnityEngine;

namespace MainModule
{
    public class ItemSpawnPointBehaviour : MonoBehaviour
    {
        public ItemType ItemType;
        public UniqueId UniqueId;

        private void OnValidate()
        {
            name = $"Item Spawn - {ItemType}";
        }
    }
}