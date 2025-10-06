using System;
using System.Collections.Generic;
using System.Linq;
using CarritoECommerce.Core.Command;

namespace CarritoECommerce.Core.Order
{
    // Builder para construir un Pedido paso a paso.
    // Build() valida que haya Items y Direccion, como pide la consigna.
    public class PedidoBuilder
    {
        private readonly Pedido _pedido = new();

        public PedidoBuilder ConItems(List<Item> items)
        {
            // Creamos una copia superficial de la lista para evitar aliasing
            _pedido.Items = items?.Select(i => new Item
            {
                Sku = i.Sku,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity
            }).ToList() ?? new List<Item>();

            return this;
        }

        public PedidoBuilder ConDireccion(string direccion)
        {
            _pedido.Direccion = direccion;
            return this;
        }

        public PedidoBuilder ConMetodoPago(string tipoPago)
        {
            _pedido.TipoPago = tipoPago;
            return this;
        }

        public PedidoBuilder ConMonto(decimal monto)
        {
            _pedido.Monto = monto;
            return this;
        }

        public Pedido Build()
        {
            // Validaciones: Items y Direccion son obligatorios
            if (_pedido.Items == null || !_pedido.Items.Any())
                throw new InvalidOperationException("Pedido inválido: debe contener al menos un ítem.");
            if (string.IsNullOrWhiteSpace(_pedido.Direccion))
                throw new InvalidOperationException("Pedido inválido: la dirección es obligatoria.");

            // Asignar un Id simple (podrías integrar un servicio de IDs)
            _pedido.Id = new Random().Next(1000, 9999);
            _pedido.Estado = EstadoPedido.Recibido;

            return _pedido;
        }
    }
}
