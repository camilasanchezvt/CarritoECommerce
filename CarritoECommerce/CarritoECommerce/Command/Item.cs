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
        public string Nombre { get; init; } = "";
        public decimal Precio { get; init; }
        public int Cantidad { get; set; }
    }
}
}
