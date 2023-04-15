using UnityEngine;

namespace MvvmModule
{
    internal interface IPrefabFactory
    {
        T Instantiate<T>(string prefabName, Transform parent)
            where T : MonoBehaviour;
    }
}