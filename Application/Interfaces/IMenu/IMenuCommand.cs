using Domain.Entities;

namespace Application.Interfaces.IMenu
{
    public interface IMenuCommand
    {
        Menu CreateMenu(Menu menu);

        Menu AsignarPlatillo(MenuPlatillo platillo);
        Menu DeleteMenu(Menu menu);

    }
}
