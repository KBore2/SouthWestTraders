using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SouthWestTradersAPI.Domain.IServices;
using SouthWestTradersAPI.Domain.Models;

namespace SouthWestTradersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockController : Controller
    {

        private readonly IStockService service;

        public StockController(IStockService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Stock>>> GetAllStocks()
        {
            return Ok(await service.GetAllStocks());
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveStock(int id)
        {
            await service.RemoveStockById(id);
            return Ok("Stock deleted");
        }

        [HttpPost]
        public async Task<ActionResult<Stock>> AddStock(Stock Stock)
        {
            return Ok(await service.AddStock(Stock));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStockForProductId(int id)
        { 
            return Ok( await service.GetStockfForProduct(id));
        }

        [HttpPut]
        public async Task<ActionResult<Stock>> UpdateStock(Stock Stock)
        {
            return Ok(await service.UpdateStock(Stock));
        }


    }
}
