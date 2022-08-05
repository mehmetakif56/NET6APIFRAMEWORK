namespace TTBS.MongoDB
{
    public interface IMongoDBEntity
    {
    }

    public interface IMongoDBEntity<out TKey> : IMongoDBEntity where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        DateTime CreatedAt { get; set; }
    }
}
