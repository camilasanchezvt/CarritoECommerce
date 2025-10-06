using System;

namespace CarritoECommerce.Core.Payment
{
    // Implementación concreta: Pago por transferencia.
    public class PagoTransfer : IPago
    {
        public bool Procesar(decimal monto)
        {
            Console.WriteLine($"[PagoTransfer] Procesando pago por TRANSFERENCIA por ${monto:F2} (simulado) -> OK");
            return true;
        }
    }
}