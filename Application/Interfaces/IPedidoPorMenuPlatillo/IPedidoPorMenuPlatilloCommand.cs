using Domain.Entities;

namespace Application.Interfaces.IPedidoPor

{
    public interface IPedidoPorMenuOptionCommand
    {
        PedidoPorMenuOption CreatePedidoPorMenuOption(PedidoPorMenuOption pedidoPorMenuOption);

        PedidoPorMenuOption DeletePedidoPorMenuOption(Guid idPedidoPorMenuOption);
    }
}
