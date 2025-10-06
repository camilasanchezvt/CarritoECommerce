using System;
using CarritoECommerce.Core.Services;
using CarritoECommerce.Core.Order;

namespace CarritoECommerce.Core.Order.Observers
{
    // LogisticaObserver: actualiza tablero/logística (simulado con Console.WriteLine)
    public class LogisticaObserver
    {
        // Propiedad que indica si está suscripto o no
        public bool Suscripto { get; private set; } = false;

        public void Suscribir(PedidoService pedidos)
        {
            // Lógica de suscripción
            Suscripto = true;
        }

        public void Desuscribir(PedidoService pedidos)
        {
            // Lógica de desuscripción
            Suscripto = false;
        }
    }
}