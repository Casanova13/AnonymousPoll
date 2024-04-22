namespace AnonymousPoll.Interfaces
{
    public interface IInitializable<T>
    {
        public abstract void Initialize(T input);

        public bool Initialized { get; }
    }
}
