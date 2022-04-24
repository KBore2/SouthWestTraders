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
        public async Task<ActionResult<Order>> GetOrder(string name)
        {
            return Ok( await service.GetOrderByName(name));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            return Ok();
        }


    }
}
