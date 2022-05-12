using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SouthWestTradersAPI.Domain.IServices;
using SouthWestTradersAPI.Domain.Models;

namespace SouthWestTradersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {

        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return Ok(await service.GetAllProducts());
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveProduct(int id)
        {
            await service.RemoveProductById(id);
            return Ok("product deleted");
        }

        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            return Ok(await service.AddProduct(product));
        }

        [HttpGet("{name}")]
        //Task<Product> for Queries
        public async Task<Product> GetProductByName(string name)
        {
            
           var response = await service.GetProductByName(name);
           return response;
            
        }

    }
}
