using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Interfaces;

namespace CarritoECommerce.Command
{
    public class CarritoPort : ICarritoPort
    {
        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Run(ICommand cmd)
        {
            throw new NotImplementedException();
        }

        public decimal Subtotal()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
