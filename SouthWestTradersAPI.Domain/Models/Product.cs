using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SouthWestTradersAPI.Domain.Models
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
            Stocks = new HashSet<Stock>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        [JsonIgnore]
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
