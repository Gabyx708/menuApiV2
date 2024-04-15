namespace Application.Response.PedidoResponses
{
    public class PedidoGetResponse
    {
        public Guid id { get; set; }
        public Guid Personal { get; set; }
        public Guid Recibo { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
    }
}
