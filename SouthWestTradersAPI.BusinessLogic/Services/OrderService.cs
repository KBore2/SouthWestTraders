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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IStockService stockService;
        private readonly IOrderStateService orderStateService;

        public OrderService(IOrderRepository repository, IStockService stockService, IOrderStateService orderStateService)
        {
            this.repository = repository;
            this.stockService = stockService;
            this.orderStateService = orderStateService;
        }

        public async Task<Order> AddOrder(Order Order)
        {
            try
            {

                var stock = await stockService.GetStockfForProduct(Order.ProductId);
               
                if (Order.Quantity > stock.AvailableStock)
                    throw new Exception("no available stock");

                stock.AvailableStock = stock.AvailableStock - Order.Quantity;
                await stockService.UpdateStock(stock);

                return await repository.AddAsync(Order);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> CancelOrder(int id)
        {
            try
            {
                var order = await repository.GetAsync(o => o.OrderId == id);
                if (order == null)
                    throw new Exception("order not found");

                var completeOrderState = await orderStateService.GetOrderStateByState("COMPLETED");
                if(order.OrderStateId == completeOrderState.OrderStateId)
                    throw new Exception("order complete, cannot be cancelled");

                var stock = await stockService.GetStockfForProduct(order.ProductId);

                stock.AvailableStock = stock.AvailableStock + order.Quantity;
                await stockService.UpdateStock(stock);

                var cancledOrderState = await orderStateService.GetOrderStateByState("COMPLETE");
                order.OrderStateId = cancledOrderState.OrderStateId;
                return await repository.UpdateAsync(o => o.OrderId == id, order);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> CompleteOrder(int id)
        {
            try
            {
                var order = await repository.GetAsync(o => o.OrderId == id);
                if (order == null)
                    throw new Exception("order not found");

                var completeOrderState = await orderStateService.GetOrderStateByState("COMPLETE");
                order.OrderStateId = completeOrderState.OrderStateId;
                return await repository.UpdateAsync(o => o.OrderId == id, order);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await repository.ListAsync(o => true);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> GetOrderByName(string name)
        {
            try
            {
                return await repository.GetAsync(o => o.Name == name);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> GetOrdersByDate(DateTime date)  
        {
            try
            {
                return await repository.ListAsync(o => o.CreatedDateUtc <= date && o.CreatedDateUtc >= date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveOrderById(int id)
        {
            try
            {
                await repository.RemoveAsync(p => p.OrderId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
