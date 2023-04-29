using MvvmModule;

namespace StartWindowModule.View
{
    public interface IStartWindowViewModel : IViewModel
    {
        bool HasProgress { get; }
        void StartClickFromView();
        void ContinueClickFromView();
        void SettingsClickFromView();
    }
}