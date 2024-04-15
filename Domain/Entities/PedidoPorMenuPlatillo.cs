namespace Domain.Entities
{
    public class PedidoPorMenuPlatillo
    {
        public Guid IdPedido { get; set; }
        public Pedido Pedido { get; set; }
        public Guid IdMenuPlatillo { get; set; }
        public MenuPlatillo MenuPlatillo { get; set; }
    }
}
