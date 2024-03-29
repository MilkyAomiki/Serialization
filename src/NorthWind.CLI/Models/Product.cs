﻿using System;
using System.Collections.Generic;

#nullable disable

namespace NorthWind.CLI.Models
{
	[Serializable]
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public int? QuantityPerUnit { get; set; }
        public int? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public int? UnitsOnOrder { get; set; }
        public int? ReorderLevel { get; set; }
        public bool? Discounted { get; set; }
    }
}
