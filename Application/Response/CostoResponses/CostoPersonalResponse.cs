namespace Application.Response.CostoResponses
{
    public class CostoPersonalResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public DateTime InicioPeriodo { get; set; }
        public DateTime FinPeriodo { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal Descuento { get; set; }
        public int CantidadPedidos { get; set; }
    }
}
