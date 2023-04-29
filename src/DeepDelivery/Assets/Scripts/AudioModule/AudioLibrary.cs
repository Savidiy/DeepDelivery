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

        public AudioClip GetClip(SoundId soundId)
        {
            if (_audioClipDictionary.TryGetValue(soundId, out AudioClip audioClip))
            {
                return audioClip;
            }

            throw new Exception($"Can't find audio clip '{soundId}'");
        }
    }
}