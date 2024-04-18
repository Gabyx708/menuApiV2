using Domain.Entities;

namespace Application.Interfaces.IPedidoPor

{
    public interface IPedidoPorMenuOptionQuery
    {
        PedidoPorMenuOption GetPedidoPorMenuOption(Guid idPedido, Guid idMenuOption);

        List<PedidoPorMenuOption> GetPedidoMenuOptionByMenu(Guid idMenu);

        List<PedidoPorMenuOption> GetPedidoMenuOptionByPedido(Guid idPedido);
    }
}
