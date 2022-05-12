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

        public async Task<Response<Order>> AddOrder(Order Order)
        {
            var stock = await stockService.GetStockfForProduct(Order.ProductId);
            if(stock == null)
                return new Response<Order>(Model:null, Message:"no stock found",HasError: true);

            if (Order.Quantity > stock.AvailableStock)
                return new Response<Order>(null, "no stock found",true); ;
                    

            stock.AvailableStock = stock.AvailableStock - Order.Quantity;
            
            await stockService.UpdateStock(stock);
                
           var order = await repository.AddAsync(Order);
           return new Response<Order>(order, "",false);
        }

        public async Task<Order> CancelOrder(int id)
        {
           
            var order = await repository.GetAsync(o => o.OrderId == id);
                

            var completeOrderState = await orderStateService.GetOrderStateByState("COMPLETE");
                

            var stock = await stockService.GetStockfForProduct(order.ProductId);
            if (stock == null)
                new Response<Order>(Model: null, Message: "no stock found", HasError: true);

            stock.AvailableStock = stock.AvailableStock + order.Quantity;
            await stockService.UpdateStock(stock);

            var cancledOrderState = await orderStateService.GetOrderStateByState("CANCELLED");
            order.OrderStateId = cancledOrderState.OrderStateId;
            return await repository.UpdateAsync(o => o.OrderId == id, order);
        }

        public async Task<Order> CompleteOrder(int id)
        {
            
            var order = await repository.GetAsync(o => o.OrderId == id);
            if (order == null)
                throw new Exception("order not found");

            var completeOrderState = await orderStateService.GetOrderStateByState("COMPLETE");
            order.OrderStateId = completeOrderState.OrderStateId;
            return await repository.UpdateAsync(o => o.OrderId == id, order);

        }

        public async Task<List<Order>> GetAllOrders()
        {
            
            return await repository.ListAsync(o => true);
            
        }

        public async Task<Response<Order>> GetOrderByName(string name)
        {
            
            var order =  await repository.GetAsync(o => o.Name == name);
            if (order == null)
                return new Response<Order>(null, "order not found",false);

            return new Response<Order>(order,string.Empty,false);
            
        }

        public async Task<List<Order>> GetOrdersByDate(DateTime date)  
        {
            
            return await repository.ListAsync(o => o.CreatedDateUtc <= date && o.CreatedDateUtc >= date);
        }

        public async Task RemoveOrderById(int id)
        {
            await repository.RemoveAsync(p => p.OrderId == id);
        }

        
    }
}
