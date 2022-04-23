using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.IServices
{
    public interface IStockService
    {
        Task<List<Stock>> GetAllStocks();

        Task<Stock> AddStock(Stock Stock);

        Task<Stock> GetStockfForProduct(int id);

        Task<Stock> UpdateStock(Stock Stock);

        Task RemoveStockById(int id);
    }
}
