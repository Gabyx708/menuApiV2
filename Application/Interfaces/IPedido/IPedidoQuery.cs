using Domain.Entities;

namespace Application.Interfaces.IPedido
{
    public interface IPedidoQuery
    {
        Order GetPedidoById(Guid idPedido);
        List<Order> GetAll();
        List<Order> GetPedidosFiltrado(Guid? idPersonal, DateTime? fechaDesde, DateTime? fechaHasta, int? ultimos);
    }
}
