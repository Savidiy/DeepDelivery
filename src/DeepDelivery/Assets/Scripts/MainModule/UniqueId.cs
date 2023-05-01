using System;
using UnityEngine;

namespace MainModule
{
    public class UniqueId : MonoBehaviour
    {
        public string Id;

        public void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
                GenerateId();
        }

        public void GenerateId()
        {
            Id = $"{gameObject.scene.name}-{Guid.NewGuid().ToString()}";
        }
    }
}