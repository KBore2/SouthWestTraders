using SouthWestTradersAPI.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Domain.IRepository
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}
