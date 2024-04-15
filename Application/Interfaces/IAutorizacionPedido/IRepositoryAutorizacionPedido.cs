using Domain.Entities;

namespace Application.Interfaces.IAutorizacionPedido
{
    public interface IRepositoryAutorizacionPedido
    {
        AutorizacionPedido CreateAutorizacionPedido(AutorizacionPedido entity);
        AutorizacionPedido DeleteAutorizacionPedido(Guid idPedido, Guid idPersonal);
        AutorizacionPedido GetAutorizacionPedidoByidPedido(Guid idPedido);
        List<AutorizacionPedido> GetAutorizacionesPedidoByIdPersonal(Guid idPersonal);
    }
}
