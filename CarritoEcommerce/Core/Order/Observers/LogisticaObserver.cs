using System;
using CarritoECommerce.Core.Services;
using CarritoECommerce.Core.Order;

namespace CarritoECommerce.Core.Order.Observers
{
    // LogisticaObserver: actualiza tablero/logística (simulado con Console.WriteLine)
    public class LogisticaObserver
    {
        public void Suscribir(PedidoService service) => service.EstadoCambiado += Handle;
        public void Desuscribir(PedidoService service) => service.EstadoCambiado -= Handle;

        private void Handle(object sender, PedidoEstadoEventArgs e)
        {
            Console.WriteLine($"[LogisticaObserver] Tablero logística: Pedido {e.Pedido.Id} -> {e.NuevoEstado}");
        }
    }
}
