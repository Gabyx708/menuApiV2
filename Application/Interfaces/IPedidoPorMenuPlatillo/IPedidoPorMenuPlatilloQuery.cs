using Domain.Entities;

namespace Application.Interfaces.IPedidoPor

{
    public interface IPedidoPorMenuOptionQuery
    {
        MenuOption GetPedidoPorMenuOption(Guid idPedido, Guid idMenuOption);

        List<MenuOption> GetPedidoMenuOptionByMenu(Guid idMenu);

        List<MenuOption> GetPedidoMenuOptionByPedido(Guid idPedido);
    }
}
