using System;
using MvvmModule;
using StartWindowModule.Contracts;
using StartWindowModule.View;
using UiModule;
using UnityEngine;

namespace StartWindowModule
{
    public sealed class StartWindowPresenter : IDisposable, IStartWindowPresenter
    {
        private const string PREFAB_NAME = "Start_window";
        private readonly WindowsRootProvider _windowsRootProvider;
        private readonly IViewFactory _viewFactory;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly StartWindowView _view;

        public StartWindowPresenter(WindowsRootProvider windowsRootProvider, IViewFactory viewFactory,
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
            var viewModel = _viewModelFactory.CreateEmptyViewModel<StartWindowViewModel>();
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

        private StartWindowView CreateView()
        {
            Transform root = _windowsRootProvider.GetWindowRoot();
            var view = _viewFactory.CreateView<StartWindowView, StartWindowHierarchy>(PREFAB_NAME, root);
            return view;
        }
    }
}