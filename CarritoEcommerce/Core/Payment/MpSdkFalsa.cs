using System;

namespace CarritoECommerce.Core.Payment
{
    // Simula un SDK externo (por ejemplo MercadoPago).
    // Tiene la API que el SDK real tendría: Cobrar(decimal).
    public class MpSdkFalsa
    {
        public bool Cobrar(decimal monto)
        {
            Console.WriteLine($"[MpSdkFalsa] SDK externo: cobrando ${monto:F2} (simulado) -> OK");
            return true;
        }
    }
}
