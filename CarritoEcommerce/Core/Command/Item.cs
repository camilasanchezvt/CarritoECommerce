using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoECommerce.Command
{
    public class Item
    {
        public string Sku { get; init; }
        public string Name { get; init; } = "";
        public decimal Price { get; init; }
        public int Quantity { get; set; }
    }
}
}
