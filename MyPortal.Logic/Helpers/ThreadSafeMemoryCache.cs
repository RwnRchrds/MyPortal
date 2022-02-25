using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MyPortal.Logic.Helpers
{
    internal class ThreadSafeMemoryCache<TItem>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        public async Task<TItem> Get(object key)
        {
            if (!_cache.TryGetValue(key, out TItem cacheEntry))
            {
                return default;
            }

            return cacheEntry;
        }

        public async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem, TimeSpan? cacheEntryLifespan = null)
        {
            if (!cacheEntryLifespan.HasValue)
            {
                // Cache entries for 8 hours by default
                cacheEntryLifespan = TimeSpan.FromHours(8);
            }

            if (!_cache.TryGetValue(key, out TItem cacheEntry))// Look for cache key.
            {
                SemaphoreSlim mylock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();
                try
                {
                    if (!_cache.TryGetValue(key, out cacheEntry))
                    {
                        // Key not in cache, so get data.
                        cacheEntry = await createItem();
                        if (cacheEntry != null)
                        {
                            _cache.Set(key, cacheEntry, cacheEntryLifespan.Value);
                        }
                    }
                }
                finally
                {
                    mylock.Release();
                }
            }
            return cacheEntry;
        }
    }
}
