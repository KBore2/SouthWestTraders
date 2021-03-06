using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.IRepository
{
    public interface IOrderStateRepository : IAsyncRepository<OrderState>
    {
        Task<IEnumerable<OrderState>> GetCachedOrderStates();


        Task<OrderState> GetCachedOrderStatesByKey(int orderStateId);


        Task<OrderState> GetCachedOrderStatesByName(string state);
    }
}
