using System;

namespace CarritoECommerce.Core.Payment
{
    // Interfaz contract (ya decías que existe en Contracts; la dejo acá por claridad).
    // Todos los tipos de pago deben implementar Procesar(decimal monto).
    public interface IPago
    {
        bool Procesar(decimal monto);
    }
}
