using Application.Response.AutorizacionPedidoResponses;
using Application.Response.MenuOptionResponses;
using Application.Response.ReciboResponses;

namespace Application.Response.PedidoResponses
{
    public class PedidoResponse
    {
        public Guid idPedido { get; set; }
        public string Nombre { get; set; }
        public DateTime fecha { get; set; }
        public AutorizacionPedidoResponse? Autorizacion { get; set; }
        public List<MenuOptionGetResponse> platillos { get; set; }
        public ReciboResponse recibo { get; set; }

    }
}
