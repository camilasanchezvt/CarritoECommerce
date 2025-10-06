using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Core.Interfaces;


namespace CarritoECommerce.Core.Command
{
    public class SetQuantityCommand
    {
        public class SetQuantityCommand : ICommand
        {
            private readonly Cart cart;
            private readonly string sku;
            private int oldQuantity = -1;
            private readonly int newQuantity;
            private bool execute = false;


            public SetQuantityCommand(Cart cart, string sku, int newQuantity) // constructor
            {
                this.cart = cart;
                this.sku = sku;
                this.newQuantity = newQuantity;
            }

            public void Execute()
            {
                if (execute) return; // si execute es verdadero, se detiene

                var item = cart.GetItem(sku); // obtiene el item o devuelve null si no existe
                if (item != null)
                {
                    oldQuantity = item.Quantity; // en caso de haber, guardamos la cantidad anterior
                    cart.SetQuantity(sku, newQuantity);
                }
                else
                {
                    oldQuantity = -1;
                }

                execute = true;
            }

            public void Undo()
            {
                if (!execute) return; // si nunca se ejecuto, no hay nada que deshacer

                if (oldQuantity == -1) // el valor no existia antes, eliminamos del carrito
                {
                    cart.RemoveItem(sku); // si no existia cantidad previa, borramos este item del carrito
                }
                else
                {
                    cart.SetQuantity(sku, oldQuantity); //si existia, lo restauramos a la cantidad anterior
                }

                execute = false; // ahora que se deshizo, declaramos ejecutado como falso
            }
        }
    }
}
