using Domain.Entities;

namespace Application.Interfaces.IPedidoPor

{
    public interface IPedidoPorMenuOptionCommand
    {
        MenuOption CreatePedidoPorMenuOption(MenuOption pedidoPorMenuOption);

        MenuOption DeletePedidoPorMenuOption(Guid idPedidoPorMenuOption);
    }
}
