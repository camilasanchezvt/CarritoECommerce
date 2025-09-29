using CarritoECommerce.Command;
using System;

namespace CarritoECommerce
{
    internal class Program
    {
        static void Main(string[] args)
        {
                var cart = new Cart();

                // 1. Crear un item
                var itemA = new Item { Sku = "A001", Name = "Producto A", Price = 1000m, Quantity = 2 };

                // 2. Agregar al carrito
                cart.AddItem(itemA);
                Console.WriteLine($"Subtotal tras agregar A001 x2: {cart.Subtotal()}"); // Esperado: 2000

                // 3. Agregar más cantidad del mismo SKU
                cart.AddItem(new Item { Sku = "A001", Name = "Producto A", Price = 1000m, Quantity = 3 });
                Console.WriteLine($"Subtotal tras agregar A001 +3: {cart.Subtotal()}"); // Esperado: 5000

                // 4. Cambiar cantidad a 1
                cart.SetCantidad("A001", 1);
                Console.WriteLine($"Subtotal tras set cantidad a 1: {cart.Subtotal()}"); // Esperado: 1000

                // 5. Quitar el ítem
                var eliminado = cart.QuitarItem("A001");
                Console.WriteLine($"Eliminado: {eliminado?.Name}, Subtotal ahora: {cart.Subtotal()}"); // Esperado: 0
            }
        }

    }
}
}
