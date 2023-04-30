using System;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    [CreateAssetMenu(fileName = "ItemSpriteProvider", menuName = "ItemSpriteProvider", order = 0)]
    public class ItemSpriteProvider : AutoSaveScriptableObject
    {
        public Sprite DefaultSprite;
        public ItemSpriteDictionary ItemSpriteDictionary;

        public Sprite GetSprite(ItemType itemType)
        {
            if (ItemSpriteDictionary.TryGetValue(itemType, out Sprite sprite))
                return sprite;

            Debug.LogError($"Can't find sprite for '{itemType}', return default sprite");
            return DefaultSprite;
        }
    }

    [Serializable]
    public class ItemSpriteDictionary : SerializedDictionary<ItemType, Sprite>
    {
    }
}