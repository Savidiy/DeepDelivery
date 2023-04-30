using System.Collections.Generic;
using MainModule;

namespace LevelWindowModule.View
{
    public class ItemsArgs
    {
        public Dictionary<ItemType, int> Items { get; }

        public ItemsArgs(Dictionary<ItemType,int> items)
        {
            Items = items;
        }
    }
}