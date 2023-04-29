#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Savidiy.Utils
{
    public class AutoSaveScriptableObject : ScriptableObject
    {
        private const int MILLISECONDS_DELAY = 5000;
        private CancellationTokenSource _cancellationTokenSource;

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            if (EditorUtility.IsDirty(this))
                SaveAsync().Forget();
#endif
        }

        private async UniTaskVoid SaveAsync()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            await UniTask.Delay(MILLISECONDS_DELAY, cancellationToken: _cancellationTokenSource.Token);

#if UNITY_EDITOR
            AssetDatabase.SaveAssetIfDirty(this);
#endif
        }

        protected void SavePrefab()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
#endif
        }
    }
}