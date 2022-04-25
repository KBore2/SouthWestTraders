using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SouthWestTradersAPI.Domain.Models
{
    public partial class Stock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int AvailableStock { get; set; }

        [JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
