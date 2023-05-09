using UiModule;
using UnityEngine;
using UnityEngine.UI;

namespace LevelWindowModule.View
{
    public sealed class LevelWindowHierarchy : MonoBehaviour
    {
        public Button SettingsButton;
        public Button RestartLevelButton;
        public Button LoadLevelButton;
        public HeartHierarchy HeartPrefab;
        public Transform HeartRoot;
        public ItemsHierarchy ItemsHierarchy;
        public Transform QuestsRoot;
        public GameObject MobileInput;
        public GameObject CompletedGame;
        public MoveStickBehaviour MoveStick;
        public PressButton FireButton;
    }
}