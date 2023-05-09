using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    [CreateAssetMenu(fileName = nameof(InputStaticData), menuName = nameof(InputStaticData), order = 0)]
    public class InputStaticData : AutoSaveScriptableObject
    {
        public KeyCode[] RightKeys;
        public KeyCode[] LeftKeys;
        public KeyCode[] UpKeys;
        public KeyCode[] DownKeys;
        public KeyCode[] ShootKeys;
    }
}