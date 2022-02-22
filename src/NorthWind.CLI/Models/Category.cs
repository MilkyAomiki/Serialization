using System;
using System.Collections.Generic;

#nullable disable

namespace NorthWind.CLI.Models
{
	[Serializable]
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
