using UnityEngine;
using Zenject;

namespace MvvmModule
{
    internal sealed class ViewFactory : IViewFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly IPrefabFactory _prefabFactory;

        public ViewFactory(IInstantiator instantiator, IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
            _instantiator = instantiator;
        }

        public TView CreateView<TView, THierarchy>(THierarchy hierarchy)
            where TView : View<THierarchy>
            where THierarchy : MonoBehaviour
        {
            var view = _instantiator.Instantiate<TView>(new object[]{hierarchy});
            return view;
        }

        public TView CreateView<TView, THierarchy>(string prefabName, Transform parent)
            where TView : View<THierarchy>
            where THierarchy : MonoBehaviour
        {
            var hierarchy = _prefabFactory.Instantiate<THierarchy>(prefabName, parent);

            return CreateView<TView, THierarchy>(hierarchy);
        }
    }
}