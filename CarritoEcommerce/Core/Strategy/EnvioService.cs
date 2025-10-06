using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Core.Interfaces;
using Etapa2Envios.Core.Singleton;


namespace Etapa2Envios.Core.Strategy
{
 
        public class EnvioService
        {
            private IEnvioStrategy _actual;

            public void SetStrategy(IEnvioStrategy s)
            {
                _actual = s;
            }

            public decimal Calcular(decimal subtotal)
            {
                return _actual.Calcular(subtotal);
            }

            public string NombreActual => _actual.Nombre;
        }
}

