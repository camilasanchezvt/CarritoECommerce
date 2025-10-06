using System;
using CarritoECommerce.Core.Order;

namespace CarritoECommerce.Core.Services
{
    // EventArgs personalizado para los cambios de estado
    public class PedidoEstadoEventArgs : EventArgs
    {
        public Pedido Pedido { get; set; }
        public EstadoPedido NuevoEstado { get; set; }
    }

    // Servicio que centraliza cambios de estado y emite eventos
    public class PedidoService
    {
        // Evento que los observers pueden suscribir
        public event EventHandler<PedidoEstadoEventArgs> EstadoCambiado;

        // Cambia estado del pedido y notifica a los observadores
        public void CambiarEstado(Pedido pedido, EstadoPedido nuevoEstado)
        {
            pedido.Estado = nuevoEstado;
            Console.WriteLine($"[PedidoService] Pedido {pedido.Id} -> {nuevoEstado}");
            EstadoCambiado?.Invoke(this, new PedidoEstadoEventArgs { Pedido = pedido, NuevoEstado = nuevoEstado });
        }
    }
}
