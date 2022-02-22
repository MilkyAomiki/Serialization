using System;
using System.Collections.Generic;

#nullable disable

namespace NorthWind.CLI.Models
{
	[Serializable]
    public partial class OrderDetail
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public int? Discount { get; set; }
    }
}
