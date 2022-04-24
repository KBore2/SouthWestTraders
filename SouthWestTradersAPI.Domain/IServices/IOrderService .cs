using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.IServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();

        Task<Order> AddOrder(Order Order);

        Task<Order> GetOrderByName(string name);

        Task<List<Order>> GetOrdersByDate(DateTime date);

        Task<Order> CancelOrder(int id);

        Task<Order> CompleteOrder(int id);

        Task RemoveOrderById(int id);
    }
}
