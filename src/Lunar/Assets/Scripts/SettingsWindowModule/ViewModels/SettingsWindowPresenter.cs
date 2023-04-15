using System;
using MvvmModule;
using Savidiy.Utils;
using SettingsWindowModule.Contracts;
using SettingsWindowModule.View;
using UiModule;
using UnityEngine;

namespace SettingsWindowModule
{
    public sealed class SettingsWindowPresenter : IDisposable, ISettingsWindowPresenter
    {
        private const string PREFAB_NAME = "Settings_window";
        private readonly WindowsRootProvider _windowsRootProvider;
        private readonly IViewFactory _viewFactory;
        private readonly IViewModelFactory _viewModelFactory;
        private readonly TickInvoker _tickInvoker;
        private readonly SettingsWindowView _view;
        private SettingsWindowViewModel _viewModel;

        public SettingsWindowPresenter(WindowsRootProvider windowsRootProvider, IViewFactory viewFactory,
            IViewModelFactory viewModelFactory, TickInvoker tickInvoker)
        {
            _viewFactory = viewFactory;
            _viewModelFactory = viewModelFactory;
            _tickInvoker = tickInvoker;
            _windowsRootProvider = windowsRootProvider;
            _view = CreateView();
            _view.SetActive(false);
        }

        public void ShowWindow()
        {
            if (_viewModel != null)
                _viewModel.NeedClose -= HideWindow;

            _viewModel = _viewModelFactory.CreateEmptyViewModel<SettingsWindowViewModel>();
            _viewModel.NeedClose += HideWindow;
            _view.Initialize(_viewModel);
            _view.Hierarchy.transform.SetAsLastSibling();
            _view.SetActive(true);

            _tickInvoker.SetPause(true);
        }

        public void HideWindow()
        {
            _viewModel.NeedClose -= HideWindow;
            _view.ClearViewModel();
            _view.SetActive(false);
            _tickInvoker.SetPause(false);
        }

        public void Dispose()
        {
            if (_viewModel != null)
                _viewModel.NeedClose -= HideWindow;

            _view.Dispose();
        }

        private SettingsWindowView CreateView()
        {
            Transform root = _windowsRootProvider.GetWindowRoot();
            var view = _viewFactory.CreateView<SettingsWindowView, SettingsWindowHierarchy>(PREFAB_NAME, root);
            return view;
        }
    }
}