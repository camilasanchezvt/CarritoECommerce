using System;
using System.Collections.Generic;
using CarritoECommerce.Core.Interfaces;
using CarritoECommerce.Core.Command;     // Item, AddItemCommand, SetQuantityCommand, RemoveItemCommand, Cart
using CarritoECommerce.Core.Order;
using CarritoECommerce.Core.Payment;
using CarritoECommerce.Core.Services;
using Etapa2Envios.Core.Strategy;        // IEnvioStrategy

namespace CarritoECommerce.Core.Facade
{
    // Facade que orquesta carrito, envíos, pagos y pedidos.
    // El constructor recibe una implementación de ICartPort (ej: CartAdapter),
    // la estrategia de envío inicial y el PedidoService para notificaciones.
    public class CheckoutFacade
    {
        private readonly ICartPort _carritoPort;
        private readonly CartAdapter _cartAdapter; // para crear comandos que requieren Cart
        private IEnvioStrategy _envioActual;
        private readonly PedidoService _pedidoService;

        // Constructor: recibe ICartPort pero intenta castear a CartAdapter para acceder al Cart
        // (alternativa: pasar directamente CartAdapter desde Program).
        public CheckoutFacade(ICartPort carrito, IEnvioStrategy envioInicial, PedidoService pedidoService)
        {
            _carritoPort = carrito ?? throw new ArgumentNullException(nameof(carrito));
            _envioActual = envioInicial ?? throw new ArgumentNullException(nameof(envioInicial));
            _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));

            // Intentamos obtener el Cart interno (para construir comandos que esperan Cart)
            _cartAdapter = carrito as CartAdapter;
            if (_cartAdapter == null)
            {
                throw new ArgumentException("El ICartPort pasado debe ser un CartAdapter para construir comandos.");
            }
        }

        // Agrega un item al carrito: construye Item y ejecuta AddItemCommand
        public void AgregarItem(string sku, string name, decimal price, int quantity)
        {
            var item = new Item { Sku = sku, Name = name, Price = price, Quantity = quantity };
            var cmd = new AddItemCommand(_cartAdapter.InternalCart, item); // comando ya existente
            _carritoPort.Run(cmd);
            Console.WriteLine($"[Facade] Agregado {quantity} x {name} (sku:{sku})");
        }

        // Cambia cantidad: ejecuta SetQuantityCommand (usa la clase que ya tenés)
        public void CambiarCantidad(string sku, int cantidad)
        {
            // Asegurate de tener el using correcto arriba:
            // using CarritoECommerce.Core.Command;

            // Instanciamos el comando directamente, sin repetir namespace
            var cmd = new SetQuantityCommand(_cartAdapter.InternalCart, sku, cantidad);
            _carritoPort.Run(cmd);

            Console.WriteLine($"[Facade] Cambiada cantidad de {sku} a {cantidad}");
        }

        // Quitar item: ejecuta RemoveItemCommand
        public void QuitarItem(string sku)
        {
            var cmd = new RemoveitemCommand.RemoveItemCommand(_cartAdapter.InternalCart, sku);
            _carritoPort.Run(cmd);
            Console.WriteLine($"[Facade] Quitar item {sku}");
        }

        // Cambiar estrategia de envío (Strategy)
        public void ElegirEnvio(IEnvioStrategy nueva)
        {
            _envioActual = nueva ?? throw new ArgumentNullException(nameof(nueva));
            Console.WriteLine($"[Facade] Envío seleccionado: {_envioActual.Nombre}");
        }

        // CalcularTotal: subtotal + costo de envío (NO aplica IVA aquí)
        public decimal CalcularTotal()
        {
            decimal subtotal = _carritoPort.Subtotal();
            decimal envio = _envioActual?.Calcular(subtotal) ?? 0m;
            decimal total = subtotal + envio;
            Console.WriteLine($"[Facade] Subtotal: {subtotal:F2}, Envío: {envio:F2}, Total (sin IVA): {total:F2}");
            return total;
        }

        // Pagar: crea base (Factory o Adapter), aplica Decorators y procesa con monto = CalcularTotal()
        public bool Pagar(string tipoPago, bool aplicarIVA, decimal? cupon = null)
        {
            // Crear la base del pago
            IPago pago;
            if (tipoPago == "mp-adapter")
            {
                // Cuando se usa el adapter, creamos el adapter aquí (según consigna)
                pago = new PagoAdapterMp(new MpSdkFalsa());
            }
            else
            {
                pago = PagoFactory.Create(tipoPago);
            }

            // Decoradores según parámetros
            if (aplicarIVA)
            {
                pago = new PagoConImpuesto(pago);
            }
            if (cupon.HasValue)
            {
                pago = new PagoConCupon(pago, cupon.Value);
            }

            // Procesar pago con el total calculado (NO incluimos IVA en CalcularTotal)
            decimal monto = CalcularTotal();
            Console.WriteLine($"[Facade] Iniciando pago con '{tipoPago}' sobre ${monto:F2}");
            bool resultado = pago.Procesar(monto);
            Console.WriteLine($"[Facade] Resultado del pago: {resultado}");
            return resultado;
        }

        // ConfirmarPedido: construye pedido con Builder y simula workflow notificando via PedidoService
        public Pedido ConfirmarPedido(string direccion, string tipoPago)
        {
            // Obtengo items actuales: asumo que CartAdapter.InternalCart tiene un método DumpItems() o similar
            // Recomendación: agrega en Cart => public List<Item> DumpItems() { return _items.Values.Select(...).ToList(); }
            var items = _cartAdapter.InternalCart.DumpItems();

            var builder = new PedidoBuilder()
                .ConItems(items)
                .ConDireccion(direccion)
                .ConMetodoPago(tipoPago)
                .ConMonto(CalcularTotal());

            var pedido = builder.Build();
            Console.WriteLine($"[Facade] Pedido creado Id={pedido.Id}, Monto={pedido.Monto:F2}. Iniciando workflow...");

            // Simulación de workflow: se usan PedidoService para notificar observers
            _pedidoService.CambiarEstado(pedido, EstadoPedido.Recibido);
            _pedidoService.CambiarEstado(pedido, EstadoPedido.Preparando);
            _pedidoService.CambiarEstado(pedido, EstadoPedido.Enviado);
            _pedidoService.CambiarEstado(pedido, EstadoPedido.Entregado);

            return pedido;
        }

        // Exponer Undo/Redo/Subtotal delegados al ICartPort
        public void Undo() => _carritoPort.Undo();
        public void Redo() => _carritoPort.Redo();
        public decimal Subtotal() => _carritoPort.Subtotal();
    }
}
