using Domain.Entities;

namespace Application.Interfaces.IPedido
{
    public interface IPedidoCommand
    {
        Pedido createPedido(Pedido pedido);
        Pedido DeletePedido(Guid idPedido);

    }
}
