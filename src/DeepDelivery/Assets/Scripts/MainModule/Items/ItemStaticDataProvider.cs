using System;
using Savidiy.Utils;
using UnityEngine;

namespace MainModule
{
    [CreateAssetMenu(fileName = "ItemSpriteProvider", menuName = "ItemSpriteProvider", order = 0)]
    public class ItemStaticDataProvider : AutoSaveScriptableObject
    {
        public ItemStaticData DefaultData;
        public ItemSpriteDictionary ItemSpriteDictionary;

        public ItemStaticData GetData(ItemType itemType)
        {
            if (ItemSpriteDictionary.TryGetValue(itemType, out ItemStaticData sprite))
                return sprite;

            Debug.LogError($"Can't find sprite for '{itemType}', return default sprite");
            return DefaultData;
        }
    }

    [Serializable]
    public class ItemSpriteDictionary : SerializedDictionary<ItemType, ItemStaticData>
    {
    }

    [Serializable]
    public class ItemStaticData
    {
        public Sprite Sprite;
        public ItemBehaviour Prefab;
    }
}