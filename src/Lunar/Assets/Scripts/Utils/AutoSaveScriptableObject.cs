#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Savidiy.Utils
{
    public class AutoSaveScriptableObject : ScriptableObject
    {
        private const int ORDER = -1;
        
        [NonSerialized, ShowInInspector, HorizontalGroup, PropertyOrder(ORDER)]
        private bool _autoSave = true;

        [Button, HorizontalGroup, HideIf(nameof(_autoSave)), PropertyOrder(ORDER)]
        public void Save()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
#endif
        }

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            if (_autoSave)
                AssetDatabase.SaveAssetIfDirty(this);
#endif
        }
    }
}