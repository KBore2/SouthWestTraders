using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SouthWestTradersAPI.Domain.IServices;
using SouthWestTradersAPI.Domain.Models;

namespace SouthWestTradersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {

        private readonly IOrderService service;

        public OrderController(IOrderService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            return Ok(await service.GetAllOrders());
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveOrder(int id)
        {
            await service.RemoveOrderById(id);
            return Ok("Order deleted");
        }

        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder(Order Order)
        {
            return Ok(await service.AddOrder(Order));
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Order>> GetOrderByName(string name)
        {
            return Ok( await service.GetOrderByName(name));
        }


        [HttpGet("{date}")]
        public async Task<ActionResult<Order>> GetOrdersByDate(DateTime date)
        {
            return Ok(await service.GetOrdersByDate(date));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> CancelOrder(int id)
        {
            return Ok(await service.CancelOrder(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> CompleteOrder(int id)
        {
            return Ok(await service.CompleteOrder(id));
        }


    }
}
