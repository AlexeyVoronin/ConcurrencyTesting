using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Asteros.Abc.Common.Collections;
using Asteros.Abc.Common.Threading;
using ChessTest;

namespace Asteros.Abc.Common.Factories
{
    internal sealed class SynchronizedFactoryCache<TKey, TValue> : IFactory<TKey, TValue>, IDisposable
    {
        public SynchronizedFactoryCache(IFactory<TKey, TValue> factory)
        {
            _factory = factory;
        }

        public TValue Create(TKey key)
        {
            var value = default(TValue);

            if (!_readerWriterLock.WithReadSynchronized(
            	() => AccessManager.Invoke(o => o.TryGetValue(key, out value))))
            {
                value = _readerWriterLock.WithWriteSynchronized(
            		() => AccessManager.Invoke(o => o.GetOrCreate(key, () => _factory.Create(key))));
            }

            return value;
        }

        public IEnumerable<TValue> GetCachedValues()
        {
            return _readerWriterLock.WithReadSynchronized(
        		() => AccessManager.Invoke(o => o.Values.Distinct().ToArray()));
        }

        public void Dispose()
        {
            _readerWriterLock.Dispose();
        }

        internal readonly ThreadAccessManager<IDictionary<TKey, TValue>> AccessManager = 
        	new ThreadAccessManager<IDictionary<TKey, TValue>>(
        		new Dictionary<TKey, TValue>());
        
        private readonly IFactory<TKey, TValue> _factory;
        private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
    }
}
