using System;
using UiModule;
using UnityEngine;
using Zenject;

namespace MvvmModule
{
    public sealed class A123Hierarchy : MonoBehaviour
    {
    }

    public sealed class A123View : View<A123Hierarchy, IA123ViewModel>
    {
        // _view = CreateView<A123View, A123Hierarchy>(Hierarchy);
        // _view = CreateView<A123View, A123Hierarchy>(PREFAB_NAME, root);
        public A123View(A123Hierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(IA123ViewModel viewModel)
        {
        }
    }

    public interface IA123ViewModel : IViewModel
    {
    }

    public sealed class A123ViewModel : EmptyViewModel, IA123ViewModel
    {
        // var viewModel = CreateEmptyViewModel<A123ViewModel>();
    }

    public sealed class A123Presenter : IDisposable, IA123Presenter
    {
        // Container.BindInterfacesTo<A123Presenter>().AsSingle();
        private const string PREFAB_NAME = "_window";
        private readonly WindowsRootProvider _windowsRootProvider;
        private readonly IViewFactory _viewFactory;
        private readonly IInstantiator _instantiator;
        private readonly A123View _view;

        public A123Presenter(WindowsRootProvider windowsRootProvider, IViewFactory viewFactory, IInstantiator instantiator)
        {
            _viewFactory = viewFactory;
            _instantiator = instantiator;
            _windowsRootProvider = windowsRootProvider;
            _view = CreateView();
            _view.SetActive(false);
        }

        public void ShowWindow()
        {
            var viewModel = _instantiator.Instantiate<A123ViewModel>();
            _view.Initialize(viewModel);
            _view.SetActive(true);
        }

        public void HideWindow()
        {
            _view.ClearViewModel();
            _view.SetActive(false);
        }

        public void Dispose()
        {
            _view.Dispose();
        }

        private A123View CreateView()
        {
            Transform root = _windowsRootProvider.GetWindowRoot();
            var view = _viewFactory.CreateView<A123View, A123Hierarchy>(PREFAB_NAME, root);
            return view;
        }
    }

    public interface IA123Presenter
    {
        void ShowWindow();
        void HideWindow();
    }
}