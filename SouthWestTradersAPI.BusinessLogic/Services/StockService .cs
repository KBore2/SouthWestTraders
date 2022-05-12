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
    public class StockService : IStockService
    {
        private readonly IStockRepository repository;

        public StockService(IStockRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Stock> AddStock(Stock Stock)
        {
            
              return await repository.AddAsync(Stock);
            
        }

        public async Task<List<Stock>> GetAllStocks()
        {
           
             return await repository.ListAsync(p => p.Equals(p));
           
        }

        public async Task<Stock> GetStockfForProduct(int id)
        {
            
             return await repository.GetAsync(s => s.ProductId == id);
            
           
        }

        public async Task RemoveStockById(int id)
        {
            
           await repository.RemoveAsync(p => p.StockId == id);
          
        }

        public async Task<Stock> UpdateStock(Stock Stock)
        {
            
           return await repository.UpdateAsync(s => s.StockId == Stock.StockId,Stock);
           
        }
    }
}
