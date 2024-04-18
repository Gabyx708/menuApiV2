using Domain.Entities;

namespace Application.Interfaces.IMenuOption
{
    public interface IMenuOptionCommand
    {
        MenuOption CreateMenuOption(MenuOption MenuOption);

        MenuOption AsignarPlatilloAMenu(Guid idMenu, int idPlatillo, int stock);
        MenuOption UpdateMenuOption(Guid id, MenuOption MenuOption);

    }
}
