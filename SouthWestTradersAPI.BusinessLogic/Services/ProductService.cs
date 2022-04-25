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
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;
        private readonly IStockService stockService;

        public ProductService(IProductRepository repository, IStockService stockService)
        {
            this.repository = repository;
            this.stockService = stockService;
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                
                var prod = await repository.AddAsync(product);
                var stock = await stockService.AddStock(new Stock { ProductId = prod.ProductId });
                return prod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await repository.ListAsync(p => p.Equals(p));
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductByName(string name)
        {
            try
            {
                return await repository.GetAsync(p => p.Name == name);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveProductById(int id)
        {
            try
            {
                
                await repository.RemoveAsync(p => p.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
