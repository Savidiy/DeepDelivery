using MvvmModule;

namespace StartWindowModule.View
{
    public sealed class StartWindowView : View<StartWindowHierarchy, IStartWindowViewModel>
    {
        public StartWindowView(StartWindowHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(IStartWindowViewModel viewModel)
        {
            Hierarchy.ContinueButton.gameObject.SetActive(viewModel.HasProgress);
            Hierarchy.StartButton.gameObject.SetActive(!viewModel.HasProgress);

            BindClick(Hierarchy.StartButton, OnStartButtonClick);
            BindClick(Hierarchy.ContinueButton, OnContinueButtonClick);
            BindClick(Hierarchy.SettingsButton, OnSettingsButtonClick);
        }

        private void OnSettingsButtonClick()
        {
            ViewModel.SettingsClickFromView();
        }

        private void OnContinueButtonClick()
        {
            ViewModel.ContinueClickFromView();
        }

        private void OnStartButtonClick()
        {
            ViewModel.StartClickFromView();
        }
    }
}