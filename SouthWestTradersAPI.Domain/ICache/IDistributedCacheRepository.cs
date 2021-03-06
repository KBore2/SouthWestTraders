using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.ICache
{
    public interface IDistributedCacheRepository
    {
        Task<string> GetAsync(string cacheKey);

        Task SetAsync(string cacheKey, string cacheValue, DistributedCacheEntryOptions cacheEntryOptions);

        Task PurgeAsync(string cacheKey);
    }
}
