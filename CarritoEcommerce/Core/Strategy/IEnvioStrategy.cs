﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etapa2Envios.Core.Singleton;


namespace Etapa2Envios.Core.Strategy
{
        public interface IEnvioStrategy
        {
            string Nombre { get; }
            decimal Calcular(decimal subtotal);
        }

}

