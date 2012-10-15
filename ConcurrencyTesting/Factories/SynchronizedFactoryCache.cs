using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Collections;
using Threading;

namespace Factories
{
    public sealed class SynchronizedFactoryCache<TKey, TValue> : IFactory<TKey, TValue>, IDisposable
    {
        public SynchronizedFactoryCache(
            IDictionary<TKey, TValue> cache,
            IFactory<TKey, TValue> factory)
        {
            _cache = cache;
            _factory = factory;
        }

        public TValue Create(TKey key)
        {
            var value = default(TValue);

            if (!_readerWriterLock.WithReadSynchronized(() => _cache.TryGetValue(key, out value)))
            {
                value = _readerWriterLock.WithWriteSynchronized(
                    () => _cache.GetOrCreate(key, () => _factory.Create(key)));
            }

            return value;
        }

        public IEnumerable<TValue> GetCachedValues()
        {
            return _readerWriterLock.WithReadSynchronized(() => _cache.Values.Distinct().ToArray());
        }

        public void Dispose()
        {
            _readerWriterLock.Dispose();
        }

        private readonly IDictionary<TKey, TValue> _cache;
        private readonly IFactory<TKey, TValue> _factory;
        private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
    }
}
