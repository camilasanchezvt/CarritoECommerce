using System;
using CarritoECommerce.Core.Services;
using CarritoECommerce.Core.Order;

namespace CarritoECommerce.Core.Order.Observers
{
    // AuditoriaObserver: registra timestamp de cada cambio (simulado)
    public class AuditoriaObserver
    {
        public void Suscribir(PedidoService service) => service.EstadoCambiado += Handle;
        public void Desuscribir(PedidoService service) => service.EstadoCambiado -= Handle;

        private void Handle(object sender, PedidoEstadoEventArgs e)
        {
            Console.WriteLine($"[AuditoriaObserver] {DateTime.Now:O} - Auditoría: Pedido {e.Pedido.Id} cambió a {e.NuevoEstado}");
        }
    }
}
