namespace Domain.Entities
{
    public class Recibo
    {
        public Guid IdRecibo { get; set; }
        public Guid IdDescuento { get; set; }
        public decimal precioTotal { get; set; }

        public long? NumeroPago { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
        public Descuento descuento { get; set; }
        public Pago? pago { get; set; }

    }
}
