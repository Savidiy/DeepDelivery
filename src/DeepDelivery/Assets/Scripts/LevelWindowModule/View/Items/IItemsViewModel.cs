using System.Collections.Generic;
using MvvmModule;
using UnityEngine;

namespace LevelWindowModule.View
{
    public interface IItemsViewModel : IViewModel
    {
        IReadOnlyList<Sprite> ItemSprites { get; }
    }
}