using System;

namespace CarritoECommerce.Core.Payment
{
    // Decorator: aplica descuento (porcentaje) y delega al inner
    public class PagoConCupon : IPago
    {
        private readonly IPago _inner;
        private readonly decimal _porcentaje; // 0.10m = 10%

        public PagoConCupon(IPago inner, decimal porcentaje)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _porcentaje = porcentaje;
        }

        public bool Procesar(decimal monto)
        {
            decimal total = monto * (1 - _porcentaje);
            Console.WriteLine($"[PagoConCupon] Cupón {_porcentaje:P0} aplicado. Monto {monto:F2} -> Total con cupón {total:F2}");
            return _inner.Procesar(total);
        }
    }
}
