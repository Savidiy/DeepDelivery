namespace MvvmModule
{
    public abstract class ViewModel<TModel> : DisposableCollector, IViewModel
    {
        public TModel Model { get; }

        protected ViewModel(TModel model)
        {
            Model = model;
        }
    }
}