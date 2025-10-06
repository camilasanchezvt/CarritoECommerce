using System;
using System.Collections.Generic;
using CarritoECommerce.Core.Interfaces; // ICartPort, ICommand
using CarritoECommerce.Core.Command;    // Cart, Item

namespace CarritoECommerce.Core.Facade
{
    // Adapter / implementación concreta de ICartPort que contiene un Cart real.
    // Los comandos que ya tenés (AddItemCommand, SetQuantityCommand, RemoveItemCommand)
    // esperan un Cart en su constructor: aquí mantenemos ese Cart interno.
    public class CartAdapter : ICartPort
    {
        // Carrito real (mantiene items y operaciones reales)
        public Cart InternalCart { get; } = new();

        // Historial de comandos para Undo/Redo
        private readonly Stack<ICommand> _undo = new();
        private readonly Stack<ICommand> _redo = new();

        // Ejecuta un comando: lo ejecuta, lo guarda en Undo y limpia Redo
        public void Run(ICommand cmd)
        {
            if (cmd == null) return;
            cmd.Execute();
            _undo.Push(cmd);
            _redo.Clear();
        }

        // Deshace último comando
        public void Undo()
        {
            if (_undo.Count == 0) return;
            var cmd = _undo.Pop();
            cmd.Undo();
            _redo.Push(cmd);
        }

        // Rehace el último deshecho
        public void Redo()
        {
            if (_redo.Count == 0) return;
            var cmd = _redo.Pop();
            cmd.Execute();
            _undo.Push(cmd);
        }

        // Subtotal delega al Cart interno
        public decimal Subtotal()
        {
            return InternalCart.Subtotal();
        }

        // Helper: retorna lista de items actuales (copia) — útil para el PedidoBuilder
        public List<Item> GetItems()
        {
            // No existe GetItems en tu Cart, pero sí GetItem. Para construir la lista podemos
            // agregar un método local que recorra internamente (pero Cart actual guarda items en private Dictionary).
            // Para no cambiar Cart, aquí asumimos que puedes agregar un método GetAllItems en Cart o
            // bien exponerlos — si no querés tocar Cart, reimplementamos: asumamos que añades
            // un método `public List<Item> DumpItems()` en Cart que devuelve la lista.
            //
            // Si no querés modificar Cart, decime y adapto para leer de otra forma.
            throw new NotImplementedException("Implementa Cart.DumpItems() o reemplaza aquí por acceso directo a items.");
        }
    }
}
