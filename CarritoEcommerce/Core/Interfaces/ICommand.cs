using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoECommerce.Core.Interfaces
{
    internal interface ICommand
    {
        void Execute(); // ejecuta
        void Undo(); // deshace
    }
}
