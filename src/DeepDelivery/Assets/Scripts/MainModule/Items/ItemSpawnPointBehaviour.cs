using UnityEngine;

namespace MainModule
{
    public class ItemSpawnPointBehaviour : MonoBehaviour
    {
        public ItemType ItemType;

        private void OnValidate()
        {
            name = $"Item Spawn - {ItemType}";
        }
    }
}