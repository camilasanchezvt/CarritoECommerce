using System;
using DeliveryGo.Core.Carrito;
using DeliveryGo.Core.Envio;
using DeliveryGo.Core.Order;
using DeliveryGo.Core.Payment;
using DeliveryGo.Core.Facade;
using DeliveryGo.Core.Config;
using CarritoECommerce.Core.Facade;
using CarritoECommerce.Core.Order.Observers;
using CarritoECommerce.Core.Services;
using Etapa2Envios.Core.Singleton;
using Etapa2Envios.Core.Strategy;

namespace DeliveryGo
{
    class Program
    {
        static void Main(string[] args)
        {
            // ==========================================
            // 🟢 PASO 1: Inicializaciones principales
            // ==========================================

            // Configuración global del sistema
            ConfigManager.Instance.EnvioGratisDesde = 10000m; // Envío gratis a partir de $10.000
            ConfigManager.Instance.IVA = 0.21m;                // 21% de IVA

            // Interfaces base
            ICarritoPort carrito = new CarritoPort();
            IEnvioStrategy envio = new EnvioMoto(); // Estrategia de envío por defecto
            PedidoService pedidos = new PedidoService();

            // Observadores (Observer pattern)
            var clienteObs = new ClienteObserver();
            var logisticaObs = new LogisticaObserver();
            var auditoriaObs = new AuditoriaObserver();

            // Se suscriben al servicio de pedidos
            clienteObs.Suscribir(pedidos);
            logisticaObs.Suscribir(pedidos);
            auditoriaObs.Suscribir(pedidos);

            // Facade que unifica todas las operaciones del sistema
            var facade = new CheckoutFacade(carrito, envio, pedidos);

            // ==========================================
            // 🟡 PASO 2: Bucle principal del menú
            // ==========================================

            bool salir = false;

            while (!salir)
            {
                // Menú de opciones principal
                Console.WriteLine("\n=== 🛵 DELIVERY GO - MENÚ PRINCIPAL ===");
                Console.WriteLine("1. Agregar ítem");
                Console.WriteLine("2. Cambiar cantidad");
                Console.WriteLine("3. Quitar ítem");
                Console.WriteLine("4. Ver subtotal y total");
                Console.WriteLine("5. Undo");
                Console.WriteLine("6. Redo");
                Console.WriteLine("7. Elegir envío");
                Console.WriteLine("8. Pagar");
                Console.WriteLine("9. Confirmar pedido");
                Console.WriteLine("10. (Des)Suscribir Logística");
                Console.WriteLine("0. Salir");
                Console.Write("👉 Elegí una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine("------------------------------------");

                switch (opcion)
                {
                    // ==========================================================
                    // 🛒 CRUD del carrito (Command Pattern)
                    // ==========================================================
                    case "1":
                        // Agregar ítem
                        Console.Write("SKU: ");
                        string sku = Console.ReadLine();

                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine();

                        decimal precio = LeerDecimal("Precio (>0): ", 0);
                        int cantidad = LeerEntero("Cantidad (>0): ", 0);

                        facade.AgregarItem(sku, nombre, precio, cantidad);
                        Console.WriteLine("✅ Ítem agregado correctamente.");
                        break;

                    case "2":
                        // Cambiar cantidad
                        Console.Write("SKU a modificar: ");
                        sku = Console.ReadLine();

                        cantidad = LeerEntero("Nueva cantidad (>0): ", 0);
                        facade.CambiarCantidad(sku, cantidad);
                        Console.WriteLine("🔁 Cantidad actualizada.");
                        break;

                    case "3":
                        // Quitar ítem
                        Console.Write("SKU a quitar: ");
                        sku = Console.ReadLine();

                        facade.QuitarItem(sku);
                        Console.WriteLine("🗑️ Ítem eliminado.");
                        break;

                    // ==========================================================
                    // 💰 Cálculo de totales
                    // ==========================================================
                    case "4":
                        Console.WriteLine($"💵 Subtotal: ${carrito.Subtotal()}");
                        Console.WriteLine($"🚚 Total (con envío): ${facade.CalcularTotal()}");
                        break;

                    case "5":
                        carrito.Undo();
                        Console.WriteLine("↩️ Undo realizado.");
                        break;

                    case "6":
                        carrito.Redo();
                        Console.WriteLine("↪️ Redo realizado.");
                        break;

                    // ==========================================================
                    // 🚚 Strategy Pattern: cambio de envío
                    // ==========================================================
                    case "7":
                        Console.Write("Elegí envío [moto/correo/retiro]: ");
                        string tipoEnvio = Console.ReadLine().ToLower();

                        IEnvioStrategy nuevaEstrategia;

                        if (tipoEnvio == "moto")
                            nuevaEstrategia = new EnvioMoto();
                        else if (tipoEnvio == "correo")
                            nuevaEstrategia = new EnvioCorreo();
                        else
                            nuevaEstrategia = new EnvioRetiro();

                        facade.ElegirEnvio(nuevaEstrategia);
                        Console.WriteLine($"📦 Envío cambiado a: {tipoEnvio.ToUpper()}");
                        break;

                    // ==========================================================
                    // 💳 Pago con Factory + Decorator + Adapter
                    // ==========================================================
                    case "8":
                        Console.Write("Tipo de pago [tarjeta/mp/transf/mp-adapter]: ");
                        string tipoPago = Console.ReadLine().ToLower();

                        bool aplicarIVA = LeerSiNo("¿Aplicar IVA? [s/n]: ");
                        bool aplicarCupon = LeerSiNo("¿Aplicar cupón? [s/n]: ");

                        decimal? cupon = null;
                        if (aplicarCupon)
                            cupon = LeerDecimal("Porcentaje de descuento (ej. 0.10 = 10%): ", 0, 1);

                        bool resultadoPago = facade.Pagar(tipoPago, aplicarIVA, cupon);
                        Console.WriteLine(resultadoPago ? "✅ Pago aprobado." : "❌ Pago rechazado.");
                        break;

                    // ==========================================================
                    // 🧾 Confirmar pedido (Builder + Observer)
                    // ==========================================================
                    case "9":
                        Console.Write("Dirección de entrega: ");
                        string direccion = Console.ReadLine();

                        Console.Write("Tipo de pago a registrar (texto informativo): ");
                        tipoPago = Console.ReadLine();

                        var pedido = facade.ConfirmarPedido(direccion, tipoPago);
                        Console.WriteLine($"📦 Pedido #{pedido.Id} confirmado. Estado final: {pedido.Estado}");
                        break;

                    // ==========================================================
                    // 🔔 (Des)Suscribir Logística
                    // ==========================================================
                    case "10":
                        if (logisticaObs.Suscripto)
                        {
                            logisticaObs.Desuscribir(pedidos);
                            Console.WriteLine("🚫 Logística desuscripta.");
                        }
                        else
                        {
                            logisticaObs.Suscribir(pedidos);
                            Console.WriteLine("✅ Logística suscripta.");
                        }
                        break;

                    // ==========================================================
                    // 🚪 Salida del programa
                    // ==========================================================
                    case "0":
                        salir = true;
                        Console.WriteLine("👋 Saliendo de DeliveryGo...");
                        break;

                    default:
                        Console.WriteLine("❗ Opción inválida, intentá de nuevo.");
                        break;
                }

                Console.WriteLine("------------------------------------");
            }
        }

        // ==========================================================
        // 🔧 Helpers de lectura segura de datos
        // ==========================================================
        static int LeerEntero(string mensaje, int minimo)
        {
            int valor;
            do
            {
                Console.Write(mensaje);
            } while (!int.TryParse(Console.ReadLine(), out valor) || valor <= minimo);
            return valor;
        }

        static decimal LeerDecimal(string mensaje, decimal minimo, decimal maximo = decimal.MaxValue)
        {
            decimal valor;
            do
            {
                Console.Write(mensaje);
            } while (!decimal.TryParse(Console.ReadLine(), out valor) || valor <= minimo || valor >= maximo);
            return valor;
        }

        static bool LeerSiNo(string mensaje)
        {
            Console.Write(mensaje);
            string resp = Console.ReadLine().ToLower();
            return resp == "s" || resp == "si";
        }
    }
}
