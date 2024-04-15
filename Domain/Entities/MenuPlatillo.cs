namespace Domain.Entities
{
    public class MenuPlatillo
    {
        public Guid IdMenuPlatillo { get; set; }
        public Guid IdMenu { get; set; }
        public Menu Menu { get; set; }
        public int IdPlatillo { get; set; }
        public Platillo Platillo { get; set; }
        public decimal PrecioActual { get; set; }
        public int Stock { get; set; }
        public int Solicitados { get; set; }

        public IList<PedidoPorMenuPlatillo> PedidosPorMenuPlatillo { get; set; }

    }
}
