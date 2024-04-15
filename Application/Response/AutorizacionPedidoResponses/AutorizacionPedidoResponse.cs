namespace Application.Response.AutorizacionPedidoResponses
{
    public class AutorizacionPedidoResponse
    {
        public Guid Autorizador { get; set; }
        public string Nombre { get; set; }
        public Guid idPedido { get; set; }
    }
}
