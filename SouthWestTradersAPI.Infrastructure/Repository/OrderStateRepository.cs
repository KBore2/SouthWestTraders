using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SouthWestTradersAPI.Domain.ICache;
using SouthWestTradersAPI.Domain.IRepository;
using SouthWestTradersAPI.Domain.Models;
using SouthWestTradersAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Infrastructure.Repository
{
    public class OrderStateRepository : BaseRepository<OrderState>, IOrderStateRepository
    {

        private readonly IDistributedCacheRepository distributedCache;
        private readonly string cacheKey;
        private readonly int absoluteExpiration;

        public OrderStateRepository(SouthWestTradersDBContext context, IDistributedCacheRepository distributed) : base(context)
        {
            this.distributedCache = distributed;
            this.cacheKey = "OrderStates";
            this.absoluteExpiration = 5;//hoyurs
        }

        public async Task<IEnumerable<OrderState>> GetCachedOrderStates()
        {
            var cachedOrderStates = await distributedCache.GetAsync(cacheKey);
            if(!string.IsNullOrEmpty(cachedOrderStates))
            {
                var orderStates = JsonConvert.DeserializeObject<List<OrderState>>(cachedOrderStates);
                return orderStates;
            }

            var dbOrderStates = ListAsync(x => true).Result;
            
            var jsonOrderStates = JsonConvert.SerializeObject(dbOrderStates);

            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(absoluteExpiration));

            await distributedCache.SetAsync(cacheKey,jsonOrderStates,options);

            return dbOrderStates;

        }

        public async Task<OrderState> GetCachedOrderStatesByKey(int orderStateId)
        {
            var orderState = await GetCachedOrderStates();
            return orderState.FirstOrDefault(x => x.OrderStateId == orderStateId);
        }

        public async Task<OrderState> GetCachedOrderStatesByName(string state)
        {
            var orderState = await GetCachedOrderStates();
            return orderState.FirstOrDefault(x => x.State == state );
        }


    }
}
