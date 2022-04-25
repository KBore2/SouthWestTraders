using SouthWestTradersAPI.Domain.IRepository;
using SouthWestTradersAPI.Domain.IServices;
using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.BusinessLogic.Services
{
    public class OrderStateService : IOrderStateService
    {
        private readonly IOrderStateRepository repository;
       

        public OrderStateService(IOrderStateRepository repository)
        {
            this.repository = repository;
            
        }

        public Task<OrderState> AddOrderState(OrderState OrderState)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderState>> GetAllOrderStates()
        {
            throw new NotImplementedException();
        }

        public async Task<OrderState> GetOrderStateById(int id)
        {
            try
            {
                return await repository.GetAsync(os => os.OrderStateId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderState> GetOrderStateByState(string state)
        {
            try
            {
                return await repository.GetAsync(os => os.State == state);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveOrderStateById(int id)
        {
            throw new NotImplementedException();
        }

        public OrderState UpdateOrderState(OrderState OrderState)
        {
            throw new NotImplementedException();
        }

        Task<OrderState> IOrderStateService.UpdateOrderState(OrderState OrderState)
        {
            throw new NotImplementedException();
        }
    }
}
