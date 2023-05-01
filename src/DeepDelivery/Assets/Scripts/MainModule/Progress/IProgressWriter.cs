namespace MainModule
{
    public interface IProgressWriter : IProgressReader
    {
        void UpdateProgress(Progress progress);
    }
}