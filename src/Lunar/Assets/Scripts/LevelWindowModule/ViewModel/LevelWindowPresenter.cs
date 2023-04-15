using System;
using LevelWindowModule.Contracts;
using LevelWindowModule.View;
using MvvmModule;
using UiModule;
using UnityEngine;

namespace LevelWindowModule
{
    public sealed class LevelWindowPresenter : IDisposable, ILevelWindowPresenter
    {
        private const string PREFAB_NAME = "Level_window";
        private readonly WindowsRootProvider _windowsRootProvider;
        private readonly IViewFactory _viewFactory;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly LevelWindowView _view;

        public LevelWindowPresenter(WindowsRootProvider windowsRootProvider, IViewFactory viewFactory,
            IViewModelFactory viewModelFactory)
        {
            _viewFactory = viewFactory;
            _viewModelFactory = viewModelFactory;
            _windowsRootProvider = windowsRootProvider;
            _view = CreateView();
            _view.SetActive(false);
        }

        public void ShowWindow()
        {
            var viewModel = _viewModelFactory.CreateEmptyViewModel<LevelWindowViewModel>();
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