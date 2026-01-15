namespace Model.Data
{
    /// <summary>
    /// Used in configs or game objects.
    /// Means, it's requed unique identifier for logic (serialize/deserialize use Guid for example as this Id) or static data stote.
    /// </summary>
    public interface IHaveId
    {
        string Id { get; }
    }
}