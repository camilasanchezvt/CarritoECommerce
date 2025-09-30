using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Interfaces;

namespace CarritoECommerce.Command
{
    public class CartEditor  // invoker del carrito
    {
        private readonly Stack<ICommand> _undoStack = new(); // para guardar los comandos ejecutados
        private readonly Stack<ICommand> _redoStack = new(); // para los comandos que fueron deshechos

        public void Run(ICommand cmd) // metodo para ejecutar un comando 
        {
            cmd.Execute();
            _undoStack.Push(cmd); // guarda el comando en el historial de comandos ejecutados
            _redoStack.Clear(); //en cuanto entra un nuevo comando, se borra el historial de redo
        }

        public void Undo()
        {
            if (_undoStack.Count == 0) return; // no hay comandos hechos, no hay nada que deshacer
            
            var cmd = _undoStack.Pop(); // saca el ultimo comando hecho y lo guarda en cmd
            cmd.Undo(); // lo deshace
            _redoStack.Push(cmd); // lo guarda en Redo
        }

        public void Redo()
        {
            if (_redoStack.Count == 0) return; // si no hay nada deshecho, no hay nada que rehacer

            var cmd = (_redoStack.Pop()); // agarra el ultimo comando deshecho y lo guarda en cmd
            cmd.Execute(); // lo rehace
            _undoStack.Push(cmd); // lo guarda en Undo
        }
    }
}
