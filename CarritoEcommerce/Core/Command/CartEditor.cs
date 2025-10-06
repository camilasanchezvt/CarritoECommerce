using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Core.Interfaces;

namespace CarritoECommerce.Core.Command
{
    public class CartEditor  // invoker del carrito
    {
        private readonly Cart cart = new(); // carrito
        private readonly CartEditor editor = new(); // invoker

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
