using Domain.Entities;

namespace Application.Interfaces.IPedidoPorMenuPlatillo
{
    public interface IPedidoPorMenuPlatilloCommand
    {
        PedidoPorMenuPlatillo CreatePedidoPorMenuPlatillo(PedidoPorMenuPlatillo pedidoPorMenuPlatillo);

        PedidoPorMenuPlatillo DeletePedidoPorMenuPlatillo(Guid idPedidoPorMenuPlatillo);
    }
}
