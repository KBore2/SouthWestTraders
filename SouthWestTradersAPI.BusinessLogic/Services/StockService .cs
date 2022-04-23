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
            try
            {
                return await repository.AddAsync(Stock);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Stock>> GetAllStocks()
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

        public async Task<Stock> GetStockfForProduct(int id)
        {
            try
            {
                return await repository.GetAsync(s => s.ProductId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task RemoveStockById(int id)
        {
            try
            {
                await repository.RemoveAsync(p => p.StockId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Stock> UpdateStock(Stock Stock)
        {
            throw new NotImplementedException();
        }
    }
}
