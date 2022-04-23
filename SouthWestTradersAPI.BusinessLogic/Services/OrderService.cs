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
        private readonly IProductRepository productRepository;
        private readonly IStockRepository stockRepository;

        public OrderService(IOrderRepository repository, IProductRepository productRepository, IStockRepository stockRepository)
        {
            this.repository = repository;
            this.productRepository = productRepository;
            this.stockRepository = stockRepository;
        }

        public async Task<Order> AddOrder(Order Order)
        {
            try
            {

                var stock = await stockRepository.GetAsync(s => s.ProductId == Order.ProductId);
                if (stock == null)
                    throw new Exception("product not found");

                if (Order.Quantity > stock.AvailableStock)
                    throw new Exception("no available stock");

                stock.AvailableStock = stock.AvailableStock - Order.Quantity;
                await stockRepository.UpdateAsync(s => s.StockId == stock.StockId, stock);

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

                if(order.OrderStateId == 3)
                    throw new Exception("order complete, cannot be cancelled");

                var stock = await stockRepository.GetAsync(s => s.ProductId == order.ProductId);
                if (stock == null)
                    throw new Exception("product not found");

                if (order.Quantity > stock.AvailableStock)
                    throw new Exception("no available stock");

                stock.AvailableStock = stock.AvailableStock + order.Quantity;
                await stockRepository.UpdateAsync(s => s.StockId == stock.StockId, stock);

                order.OrderStateId = 2; 
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

                order.OrderStateId = 3;
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
                return await repository.ListAsync(o => o.Equals(o));
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

        public Task<Order> GetOrdersByDate(string name)
        {
            throw new NotImplementedException();
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
