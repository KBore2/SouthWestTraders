using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SouthWestTradersAPI.Domain.Models
{
    public partial class OrderState
    {
        public OrderState()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderStateId { get; set; }
        public string State { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
