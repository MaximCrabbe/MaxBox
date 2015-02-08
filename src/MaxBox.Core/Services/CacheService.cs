using System.Collections.Generic;

namespace MaxBox.Core.Services
{
    public class CacheService<TKey, TValue> : ICacheService<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _cache = new Dictionary<TKey, TValue>();

        public void Add(TKey key, TValue item)
        {
            _cache.Add(key, item);
        }

        public void Remove(TKey key)
        {
            _cache.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue item)
        {
            return _cache.TryGetValue(key, out item);
        }
    }
}