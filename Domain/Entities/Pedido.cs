namespace Domain.Entities
{
    public class Pedido
    {
        public Guid IdPedido { get; set; }
        public Guid IdPersonal { get; set; }
        public Guid IdRecibo { get; set; }
        public DateTime FechaDePedido { get; set; }

        public Personal Personal { get; set; }
        public Recibo Recibo { get; set; }
        public AutorizacionPedido? AutorizacionPedido { get; set; }


        public IList<PedidoPorMenuPlatillo> PedidosPorMenuPlatillo { get; set; }

    }
}
