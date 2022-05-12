using Microsoft.Extensions.Caching.Distributed;
using SouthWestTradersAPI.Domain.ICache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Infrastructure.Cache
{
    public class DistributedCacheRepository : IDistributedCacheRepository
    {
        private readonly IDistributedCache cache;

        public DistributedCacheRepository(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<string> GetAsync(string cacheKey)
        {
            try
            {
                var cachedValue = await cache.GetStringAsync(cacheKey);
                if(!string.IsNullOrEmpty(cachedValue))
                {
                    return cachedValue;
                }

                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task PurgeAsync(string cacheKey)
        {
            try
            {
                await cache.RemoveAsync(cacheKey);
            }
            catch(Exception)
            {

            }
        }

        public async Task SetAsync(string cacheKey, string cacheValue, DistributedCacheEntryOptions cacheEntryOptions)
        {
            try
            {
                await cache.SetStringAsync(cacheKey, cacheValue, cacheEntryOptions);
            }
            catch (Exception)
            {

            }
        }
    }
}
