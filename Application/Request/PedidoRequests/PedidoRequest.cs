namespace Application.Request.PedidoRequests
{
    public class PedidoRequest
    {
        public Guid idUsuario { get; set; }

        public List<Guid> MenuPlatillos { get; set; }
    }
}
