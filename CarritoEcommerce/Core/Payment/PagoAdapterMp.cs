using System;

namespace CarritoECommerce.Core.Payment
{
    // Adapter: adapta MpSdkFalsa a la interfaz IPago del sistema.
    // Permite encapsular el SDK para que el resto del código no lo conozca.
    public class PagoAdapterMp : IPago
    {
        private readonly MpSdkFalsa _sdk;

        public PagoAdapterMp(MpSdkFalsa sdk)
        {
            _sdk = sdk ?? throw new ArgumentNullException(nameof(sdk));
        }

        public bool Procesar(decimal monto)
        {
            Console.WriteLine("[PagoAdapterMp] Adaptando llamada al SDK externo...");
            // Delegamos la lógica al SDK simulado
            return _sdk.Cobrar(monto);
        }
    }
}
