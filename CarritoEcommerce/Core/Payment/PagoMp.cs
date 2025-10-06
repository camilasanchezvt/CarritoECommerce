using System;

namespace CarritoECommerce.Core.Payment
{
    // Implementación "nativa" de MercadoPago (simulada).
    // NOTA: si querés usar un SDK real, usarías el Adapter en su lugar.
    public class PagoMp : IPago
    {
        public bool Procesar(decimal monto)
        {
            Console.WriteLine($"[PagoMp] Procesando pago (MP Nativo) por ${monto:F2} (simulado) -> OK");
            return true;
        }
    }
}