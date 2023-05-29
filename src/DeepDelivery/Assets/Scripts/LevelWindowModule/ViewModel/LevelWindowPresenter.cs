using System;
using LevelWindowModule.Contracts;
using LevelWindowModule.View;
using MvvmModule;
using UiModule;
using UnityEngine;
using Zenject;

namespace LevelWindowModule
{
    public sealed class LevelWindowPresenter : IDisposable, ILevelWindowPresenter
    {
        private const string PREFAB_NAME = "Level_window";
        private readonly WindowsRootProvider _windowsRootProvider;
        private readonly IViewFactory _viewFactory;
        private readonly LevelWindowView _view;
        private readonly IInstantiator _instantiator;

        public LevelWindowPresenter(WindowsRootProvider windowsRootProvider, IViewFactory viewFactory, IInstantiator instantiator)
        {
            _instantiator = instantiator;
            _viewFactory = viewFactory;
            _windowsRootProvider = windowsRootProvider;
            _view = CreateView();
            _view.SetActive(false);
        }

        public void ShowWindow()
        {
            var viewModel = _instantiator.Instantiate<LevelWindowViewModel>();
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

        private LevelWindowView CreateView()
        {
            Transform root = _windowsRootProvider.GetWindowRoot();
            var view = _viewFactory.CreateView<LevelWindowView, LevelWindowHierarchy>(PREFAB_NAME, root);
            return view;
        }
    }
}