namespace MainModule
{
    public interface IFactory<T, TK>
    {
        public T Create(TK data);
    }
}