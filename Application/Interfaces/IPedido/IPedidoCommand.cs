using Domain.Entities;

namespace Application.Interfaces.IPedido
{
    public interface IPedidoCommand
    {
        Order createPedido(Order pedido);
        Order DeletePedido(Guid idPedido);

    }
}
