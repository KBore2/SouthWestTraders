using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.IServices
{
    public interface IOrderStateService
    {
        Task<List<OrderState>> GetAllOrderStates();

        Task<OrderState> AddOrderState(OrderState OrderState);

        Task<OrderState> GetOrderStateByState(string state);

        Task<OrderState> GetOrderStateById(int id);

        Task<OrderState> UpdateOrderState(OrderState OrderState);

        Task RemoveOrderStateById(int id);
        
    }
}
