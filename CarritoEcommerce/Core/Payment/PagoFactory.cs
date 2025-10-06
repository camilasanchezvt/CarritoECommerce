using System;

namespace CarritoECommerce.Core.Payment
{
    // Factory: devuelve una implementación de IPago según el 'tipo' solicitado.
    // Importante: "mp-adapter" NO se crea aquí según la consigna.
    public static class PagoFactory
    {
        public static IPago Create(string tipo)
        {
            // Protección contra null y normalización de input
            tipo = (tipo ?? string.Empty).ToLowerInvariant();

            return tipo switch
            {
                "tarjeta" => new PagoTarjeta(),
                "transf" => new PagoTransfer(),
                "mp" => new PagoMp(),
                _ => throw new ArgumentException($"Tipo de pago desconocido: {tipo}")
            };
        }
    }
}
