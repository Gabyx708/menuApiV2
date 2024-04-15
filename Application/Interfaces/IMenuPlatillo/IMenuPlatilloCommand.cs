using Domain.Entities;

namespace Application.Interfaces.IMenuPlatillo
{
    public interface IMenuPlatilloCommand
    {
        MenuPlatillo CreateMenuPlatillo(MenuPlatillo menuPlatillo);

        MenuPlatillo AsignarPlatilloAMenu(Guid idMenu, int idPlatillo, int stock);
        MenuPlatillo UpdateMenuPlatillo(Guid id, MenuPlatillo menuPlatillo);

    }
}
