using SouthWestTradersAPI.Domain.IRepository;
using SouthWestTradersAPI.Domain.Models;
using SouthWestTradersAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthWestTradersAPI.Infrastructure.Repository
{
    public class OrderStateRepository : BaseRepository<OrderState>, IOrderStateRepository
    {
        public OrderStateRepository(SouthWestTradersDBContext context) : base(context)
        {
        }
    }
}
