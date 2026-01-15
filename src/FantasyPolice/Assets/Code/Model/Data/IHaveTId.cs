namespace Model.Data
{
    public interface IHaveTId<T>
    {
        T Id { get; }
    }
}