using Application.Request.PedidoRequests;
using Application.Response.PedidoResponses;

namespace Application.Interfaces.IPedido
{
    public interface IPedidoService
    {
        PedidoResponse HacerUnpedido(PedidoRequest request);
        PedidoResponse EliminarPedido(Guid idPedido);
        PedidoResponse GetPedidoById(Guid idPedido);
        List<PedidoGetResponse> PedidoFiltrado(Guid? idPersonal, DateTime? fechaDesde, DateTime? fechaHasta, int? cantidad);
        List<PedidoResponse> PedidosDelMenu(Guid idMenu);
        List<PedidoResponse> PedidosPorFecha(DateTime fecha);
        PedidoResponse HacerUnpedidoSinRestricciones(PedidoRequest request, Guid usuarioPedidor);

    }
}
