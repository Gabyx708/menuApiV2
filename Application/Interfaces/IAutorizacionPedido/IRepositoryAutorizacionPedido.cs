using Domain.Entities;

namespace Application.Interfaces.IAutorizacionPedido
{
    public interface IRepositoryAutorizacionPedido
    {
        Authorization CreateAutorizacionPedido(Authorization entity);
        Authorization DeleteAutorizacionPedido(Guid idPedido, Guid idPersonal);
        Authorization GetAutorizacionPedidoByidPedido(Guid idPedido);
        List<Authorization> GetAutorizacionesPedidoByIdPersonal(Guid idPersonal);
    }
}
