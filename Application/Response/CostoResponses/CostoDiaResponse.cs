namespace Application.Response.CostoResponses
{
    public class CostoDiaResponse
    {
        public DateTime Fecha { get; set; }
        public decimal CostoSinDescuento { get; set; }
        public decimal CostoConDescuento { get; set; }
        public int CantidadPedidos { get; set; }
    }
}
