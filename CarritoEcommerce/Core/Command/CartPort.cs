using CarritoECommerce.Core.Interfaces;
using System.Collections.Generic;

namespace CarritoECommerce.Core.Command
{
    public class CartPort : ICartPort
    {
        private readonly Stack<ICommand> _undoStack = new();
        private readonly Stack<ICommand> _redoStack = new();

        public void Run(ICommand cmd)
        {
            cmd.Execute();
            _undoStack.Push(cmd);
            _redoStack.Clear();
        }

        public void Undo()
        {
            if (_undoStack.Count == 0) return;

            var cmd = _undoStack.Pop();
            cmd.Undo();
            _redoStack.Push(cmd);
        }

        public void Redo()
        {
            if (_redoStack.Count == 0) return;

            var cmd = _redoStack.Pop();
            cmd.Execute();
            _undoStack.Push(cmd);
        }

        public decimal Subtotal()
        {
            // Temporal: devuelve 0 hasta que se implemente el cálculo real
            return 0m;
        }
    }
}
