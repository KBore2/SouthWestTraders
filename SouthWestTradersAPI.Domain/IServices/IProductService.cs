using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.IServices
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts();

        Task<Product> AddProduct(Product product);

        Task<Product?> GetProductByName(string name);

        Task RemoveProductById(int id);
    }
}
