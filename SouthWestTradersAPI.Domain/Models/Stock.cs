﻿using System;
using System.Collections.Generic;

namespace SouthWestTradersAPI.Domain.Models
{
    public partial class Stock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int AvailableStock { get; set; } =0;

        public virtual Product Product { get; set; } = null!;
    }
}
