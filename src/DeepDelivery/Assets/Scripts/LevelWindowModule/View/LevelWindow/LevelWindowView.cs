using MvvmModule;

namespace LevelWindowModule.View
{
    public sealed class LevelWindowView : View<LevelWindowHierarchy, ILevelWindowViewModel>
    {
        public LevelWindowView(LevelWindowHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
#if !UNITY_EDITOR
            hierarchy.RestartLevelButton.gameObject.SetActive(false);
#endif
        }

        protected override void UpdateViewModel(ILevelWindowViewModel viewModel)
        {
            BindClick(Hierarchy.SettingsButton, OnSettingsClick);
            BindClick(Hierarchy.RestartLevelButton, OnRestartLevelClick);
        }

        private void OnSettingsClick()
        {
            ViewModel.SettingsClickFromView();
        }

        private void OnRestartLevelClick()
        {
            ViewModel.RestartLevelClickFromView();
        }
    }
}