using CarritoECommerce.Core.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Core.Interfaces;

namespace CarritoECommerce.Core.Command
{
    public class RemoveitemCommand
    {
        public class RemoveItemCommand : ICommand
        {
            private readonly Cart cart;
            private readonly string sku;
            private Item removed; // para guardar el item eliminado en caso de restaurarlo con Undo
            private bool executed = false; // 

            public RemoveItemCommand(Cart cart, string sku) // constructor
            {
                this.cart = cart;
                this.sku = sku;
                this.removed = null;
            }

            public void Execute()
            {
                if (executed) return;

                removed = cart.RemoveItem(sku); // eliminamos el item segun su sku y lo almacenamos en caso de necesitarlo. puede dar null si no existe
                executed = true;
            }

            public void Undo()
            {
                if (!executed) return; // 

                if (removed != null) // verificamos que haya item eliminado primero
                {
                    cart.AddItem(removed); // agregamos nuevamente el item antes eliminado
                }

                executed = false;
            }
        }
    }
}
