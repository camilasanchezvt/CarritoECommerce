using System;
using Etapa2Envios.Core.Singleton; // reutilizo tu ConfigManager singleton

namespace CarritoECommerce.Core.Payment
{
    // Decorator: aplica IVA al monto y luego delega a inner.Procesar(total)
    public class PagoConImpuesto : IPago
    {
        private readonly IPago _inner;

        public PagoConImpuesto(IPago inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public bool Procesar(decimal monto)
        {
            // Lee el IVA desde el singleton (lo agregamos a ConfigManager)
            decimal iva = ConfigManager.Instance.IVA;
            decimal total = monto * (1 + iva);

            Console.WriteLine($"[PagoConImpuesto] IVA {iva:P0} aplicado. Monto {monto:F2} -> Total con IVA {total:F2}");
            return _inner.Procesar(total);
        }
    }
}
