using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoECommerce.Command
{
    public class Carrito
    {
        private readonly Dictionary<string, Item> _items = new();

        public void AgrgarItem(Item i)
        {
            if (_items.TryGetValue(i.Sku, out var existente))
            {
                existente.Cantidad += i.Cantidad;
            }
            else
            {
                _items[i.Sku]= i;
            }
        }

        public Item QuitarItem(string Sku)
        {
            if (_items.TryGetValue(Sku, out var existente))
            {
                _items.Remove(Sku);
                return existente;
            }
            else
            {
                Console.WriteLine("[ERROR] The item doesn't exists");
                return null;
            }
        }

        public bool SetCantidad(string sku, int nuevaCantidad)
        {
            if (_items.TryGetValue(sku, out var existente))
            {
                if (nuevaCantidad > 0)
                {
                    existente.Cantidad = nuevaCantidad;
                    return true;
                }
                else
                {
                    Console.WriteLine("La cantidad debe ser mayor a 0");
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"El item con SKU: {sku} no existe dentro del carrito.");
                return false;
            }
        }

        public decimal Subtotal()
        {
            decimal total = 0;
            
            foreach (var i in _items.Values)
            {
                total += i.Precio * i.Cantidad;
            }
            return total;
        }
    }
}
