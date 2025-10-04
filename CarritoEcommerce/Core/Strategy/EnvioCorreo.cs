using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etapa2Envios.Core.Singleton;


namespace Etapa2Envios.Core.Strategy
{
    public class EnvioCorreo : IEnvioStrategy
    {
        public string Nombre => "Correo";

        public decimal Calcular(decimal subtotal)
        {
            if (subtotal >= ConfigManager.Instance.EnvioGratisDesde)
                return 0;
            return 3500m;
        }
    }
}