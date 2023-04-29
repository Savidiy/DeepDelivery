using UnityEditor;
using UnityEngine;

namespace Savidiy.Utils.Editor
{
    public static class PlayerPrefsReset
    {
        [MenuItem("Tools/Prefs/Reset player prefs")]
        public static void ResetPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Player Prefs cleared");
        }

        [MenuItem("Tools/Prefs/Reset editor prefs")]
        public static void ResetEditorPrefs()
        {
            EditorPrefs.DeleteAll();
            Debug.Log("Editor Prefs cleared");
        }
    }
}