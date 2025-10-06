using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoECommerce.Core.Command
{
    public class Cart
    {
        private readonly Dictionary<string, Item> _items = new(); // dictionary privado de items (solo el carrito tiene acceso a este)
                                                                  // Readonly para que no pueda reasignarse (apuntar a otro dictionary)

        public void AddItem(Item i) // metodo para agregar un item que toma como parametro un objet de este valor
        {
            if (_items.TryGetValue(i.Sku, out var existent)) // if que verifica que exista el sku dentro del dictionary _items
            {
                existent.Quantity += i.Quantity; // si ya existe el item dentro del carrito, le suma esta nueva cantidad.
                                                 // (ejemplo: si habia 2 items y se deben agregar 3, el resultado final es 5)
            }
            else
            {
                _items[i.Sku] = i; //si no existe, lo agrega
            }
        }

        public Item RemoveItem(string sku) // metodo para quitar item, solo requiere el sku de este
        {
            if (_items.TryGetValue(sku, out var existent)) // if que verifica que el sku del item exista dentro del dictionary
            {
                _items.Remove(sku);
                return existent;   //si existe, elimina el item y retorna existente (referencia)
            }
            else
            {
                return null;   // si no existe, retorna nulo
            }
        }

        public bool SetQuantity(string sku, int newQuantity) // metodo para actualizar la nueva cantidad del item, tomando su sku y el numero que corresponda
        {
            if (_items.TryGetValue(sku, out var existent)) // verificamos que el item exista
            {
                if (newQuantity > 0)    // si es true, ahora verificamos que es mayor a cero
                {
                    existent.Quantity = newQuantity;
                    return true;
                }
                else if (newQuantity == 0) // en caso de ser igual a cero, removemos porque no hay tal item
                {
                    _items.Remove(sku);
                    return true;
                }
                else // de no serlo, devolvemos false
                {
                    return false;
                }
            }
            else // de no existir el item, devolvemos false
            {
                return false;
            }
        }

        public decimal Subtotal() // metodo que retorna el subtotal en valor decimal
        {
            decimal total = 0; // variable para almacenar el total

            foreach (var i in _items.Values) // foreach que recorre el dictionary
            {
                total += i.Price * i.Quantity; // multiplicamos el precio por la cantidad del item y lo almacenamos en la variable
            }
            return total; // retornamos la variable con el total
        }

        public Item GetItem(string sku) // metodo que entrega un item mediante su sku (utilizado en AddItemCommand)
        {
            if (_items.TryGetValue(sku, out var item)) // verificamos que si existe
            {
                return new Item // devolvemos una copia de este item (encapsulamiento)
                {
                    Sku = item.Sku,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
            }
            return null; // si no cumple el if, devolvemos null
        }

        // ==========================================================
        // DumpItems: método para "volcar" todos los items actuales
        // ==========================================================
        public List<Item> DumpItems()
        {
            // Retorna una lista de copias de los items actuales
            return _items.Values
                         .Select(i => new Item
                         {
                             Sku = i.Sku,
                             Name = i.Name,
                             Price = i.Price,
                             Quantity = i.Quantity
                         })
                         .ToList();
        }
    }
}
