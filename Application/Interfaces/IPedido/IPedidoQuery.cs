using Domain.Entities;

namespace Application.Interfaces.IPedido
{
    public interface IPedidoQuery
    {
        Pedido GetPedidoById(Guid idPedido);
        List<Pedido> GetAll();
        List<Pedido> GetPedidosFiltrado(Guid? idPersonal, DateTime? fechaDesde, DateTime? fechaHasta, int? ultimos);
    }
}
