using System;
using AudioModule.Contracts;
using Savidiy.Utils;
using UnityEngine;

namespace AudioModule
{
    [Serializable]
    internal sealed class AudioClipDictionary : SerializedDictionary<SoundId, AudioClip>
    {
    }

    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "AudioLibrary", order = 0)]
    public class AudioLibrary : AutoSaveScriptableObject
    {
        [SerializeField] private AudioClipDictionary _audioClipDictionary;
        public AudioClip MusicClip;

        public bool TryGetClip(SoundId soundId, out AudioClip audioClip)
        {
            if (_audioClipDictionary.TryGetValue(soundId, out audioClip))
            {
                return true;
            }

            Debug.LogError($"Can't find audio clip with id '{soundId}'");
            return false;
        }
    }
}