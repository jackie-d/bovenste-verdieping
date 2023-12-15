using System;
using Microsoft.Extensions.Caching.Memory;

namespace BovensteVerdieping.Services {

    public class CacheService {

        private IMemoryCache cache;

        public CacheService() {}

        public CacheService(IMemoryCache memoryCache) {
            this.cache = memoryCache;  
        }

        public virtual void set<T>(string key, T value, int? expirationInMinutes = null) {
            // If an expiration of the cached value is requested, apply it while saving
            if ( expirationInMinutes != null ) {
                //  Create the cache memory option object with the expiration parameter
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes((double)expirationInMinutes));
                // Save the value in cache memory with expiration    
                this.cache.Set<T>(key, value, cacheEntryOptions);
            } else {
                // Save the value in cache memory without formal expiration
                this.cache.Set<T>(key, value);
            }
        }

        public virtual T get<T>(string key) {
            // Get the value from cache memory
            return this.cache.Get<T>(key);
        }

    }

}