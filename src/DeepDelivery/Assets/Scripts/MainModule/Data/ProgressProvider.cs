namespace MainModule
{
    public class ProgressProvider
    {
        public Progress Progress { get; set; }

        public ProgressProvider(ProgressSaveLoad progressSaveLoad)
        {
            Progress = progressSaveLoad.LoadProgress();
        }
    }
}