using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Interfaces;

namespace CarritoECommerce.Command
{
    public class CartPort : ICartPort
    {
        private readonly Cart cart = new(); // carrito
        private readonly CartEditor editor= new(); // invoker

        public void Run(ICommand cmd) 
        {
            editor.Run(cmd);
        }

        public decimal Subtotal()
        {
            return cart.Subtotal();
        }

        public void Undo()
        {
            editor.Undo();
        }
        public void Redo()
        {
            editor.Redo();
        }

    }
}
