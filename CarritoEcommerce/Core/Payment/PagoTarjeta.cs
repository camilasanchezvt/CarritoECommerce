using System;

namespace CarritoECommerce.Core.Payment
{
    // Implementación concreta: Pago con tarjeta.
    // "Simula" el cobro imprimiendo en consola y devolviendo true.
    public class PagoTarjeta : IPago
    {
        public bool Procesar(decimal monto)
        {
            Console.WriteLine($"[PagoTarjeta] Procesando pago con TARJETA por ${monto:F2} (simulado) -> OK");
            return true;
        }
    }
}