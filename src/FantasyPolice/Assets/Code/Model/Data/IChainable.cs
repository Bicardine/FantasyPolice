namespace Model.Data
{
    public interface IChainable<T>
    {
        T SetNext(T serializeStep);
    }
}