namespace Domain.Entities
{
    public class Order
    {
        public Guid IdOrder { get; set; }
        public Guid IdUser { get; set; }
        public Guid IdReceipt { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusCode { get; set; }
        public User User { get; set; } = null!;
        public Receipt Receipt { get; set; } = null!;
        public Authorization? AutorizacionPedido { get; set; }


        public IList<PedidoPorMenuPlatillo> PedidosPorMenuPlatillo { get; set; }

    }
}
