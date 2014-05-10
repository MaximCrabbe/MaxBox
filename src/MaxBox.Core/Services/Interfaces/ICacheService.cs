namespace MaxBox.Core.Services
{
    public interface ICacheService<TKey, TValue>
    {
        void Add(TKey key, TValue item);
        void Remove(TKey key);
        bool TryGetValue(TKey key, out TValue item);
    }
}