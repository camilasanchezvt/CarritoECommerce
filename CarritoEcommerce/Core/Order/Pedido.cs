using System;
using System.Collections.Generic;
using CarritoECommerce.Core.Command; // Item

namespace CarritoECommerce.Core.Order
{
    // Enum con los estados posibles del pedido
    public enum EstadoPedido { Recibido, Preparando, Enviado, Entregado }

    // DTO del pedido que vamos a construir con el Builder
    public class Pedido
    {
        public int Id { get; set; }               // identificador del pedido
        public List<Item> Items { get; set; }    // lista de items (usa tu clase Item)
        public string Direccion { get; set; }    // dirección de entrega
        public string TipoPago { get; set; }     // string descriptivo del método de pago
        public EstadoPedido Estado { get; set; } // estado actual del pedido
        public decimal Monto { get; set; }       // monto final pagado (o a pagar)
    }
}
