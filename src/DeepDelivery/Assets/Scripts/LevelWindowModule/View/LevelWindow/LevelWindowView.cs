using MvvmModule;

namespace LevelWindowModule.View
{
    public sealed class LevelWindowView : View<LevelWindowHierarchy, ILevelWindowViewModel>
    {
        public LevelWindowView(LevelWindowHierarchy hierarchy, IViewFactory viewFactory) : base(hierarchy, viewFactory)
        {
        }

        protected override void UpdateViewModel(ILevelWindowViewModel viewModel)
        {
            BindClick(Hierarchy.SettingsButton, OnSettingsClick);
        }

        private void OnSettingsClick()
        {
            ViewModel.SettingsClickFromView();
        }
    }
}