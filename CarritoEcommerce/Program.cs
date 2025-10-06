using System;
using Etapa2Envios.Core.Strategy;
using Etapa2Envios.Core.Singleton;

class Program
{
    static void Main()
    {
        // Configuración global
        ConfigManager.Instance.EnvioGratisDesde = 50000m;

        var envioService = new EnvioService();

        decimal subtotal1 = 42000m;
        decimal subtotal2 = 52000m;

        // Moto
        envioService.SetStrategy(new EnvioMoto());
        Console.WriteLine($"{envioService.NombreActual} (subtotal {subtotal1}): {envioService.Calcular(subtotal1)}");

        // Correo con subtotal bajo
        envioService.SetStrategy(new EnvioCorreo());
        Console.WriteLine($"{envioService.NombreActual} (subtotal {subtotal1}): {envioService.Calcular(subtotal1)}");

        // Retiro
        envioService.SetStrategy(new RetiroEnTienda());
        Console.WriteLine($"{envioService.NombreActual} (subtotal {subtotal1}): {envioService.Calcular(subtotal1)}");

        // Correo con subtotal alto
        envioService.SetStrategy(new EnvioCorreo());
        Console.WriteLine($"{envioService.NombreActual} (subtotal {subtotal2}): {envioService.Calcular(subtotal2)}");
    }
}
