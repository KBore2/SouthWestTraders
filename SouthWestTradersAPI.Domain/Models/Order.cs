using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SouthWestTradersAPI.Domain.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public int Quantity { get; set; }
        public int OrderStateId { get; set; }

        [JsonIgnore]
        public virtual OrderState? OrderState { get; set; }

        [JsonIgnore]
        public virtual Product? Product { get; set; }
    }
}
