using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarritoECommerce.Core.Interfaces;
using Etapa2Envios.Core.Singleton;


namespace Etapa2Envios.Core.Strategy
{
        public class RetiroEnTienda : IEnvioStrategy
        {
            public string Nombre => "Retiro";

            public decimal Calcular(decimal subtotal)
            {
                return 0;
            }
        }
}
