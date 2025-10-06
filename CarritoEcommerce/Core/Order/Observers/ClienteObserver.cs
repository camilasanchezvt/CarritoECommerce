using System;
using CarritoECommerce.Core.Services;
using CarritoECommerce.Core.Order;

namespace CarritoECommerce.Core.Order.Observers
{
    // ClienteObserver: muestra mensaje amistoso para el cliente
    public class ClienteObserver
    {
        // Suscribir al evento del servicio
        public void Suscribir(PedidoService service) => service.EstadoCambiado += Handle;
        public void Desuscribir(PedidoService service) => service.EstadoCambiado -= Handle;

        private void Handle(object sender, PedidoEstadoEventArgs e)
        {
            Console.WriteLine($"[ClienteObserver] Notificación al cliente: Tu pedido {e.Pedido.Id} está {e.NuevoEstado}");
        }
    }
}
