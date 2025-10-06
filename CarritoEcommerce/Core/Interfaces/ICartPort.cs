using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarritoECommerce.Core.Interfaces
{
    public interface ICartPort
    {
        decimal Subtotal(); // suma el total (precio * cantidad)
        void Run(ICommand cmd); // ejecuta un comando y lo guarda en historial
        void Undo(); // deshacer último comando
        void Redo(); // rehace el ultimo comando deshecho
    }
}
