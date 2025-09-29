using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Interfaces;

namespace CarritoECommerce.Command
{
    public class AddItemCommand : ICommand
    {
        private Cart cart; 
        private Item itemToAdd;
        private Item previousItem;
        private bool executed = false;

        public AddItemCommand(Cart cart, Item itemToAdd) // constructor
        {
            this.cart = cart;
            this.itemToAdd= itemToAdd;
        }

        public void Execute()
        {
            if (executed) return;

            previousItem = cart.GetItem(itemToAdd.Sku); // en caso de que exista un item, lo guardamos (si no hay nada, devuelve null y continua)
            cart.AddItem(itemToAdd);  // agregamos el item actual

            executed = true; 
        }

        public void Undo()
        {
            if (!executed) return;

            if (previousItem == null) // verificamos que no habia item previo
            {
                cart.RemoveItem(itemToAdd.Sku); // eliminamos el ultimo item agregado
            }
            else 
            {
                cart.RemoveItem(itemToAdd.Sku); // en caso de que si hubiera item previo, eliminamos el actual
                cart.AddItem(previousItem); // y restauramos el anterior
            }

            executed = false;
        }
    }
}
