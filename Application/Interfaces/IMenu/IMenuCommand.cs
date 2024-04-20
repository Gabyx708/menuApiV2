using Domain.Entities;

namespace Application.Interfaces.IMenu
{
    public interface IMenuCommand
    {
        Menu InsertMenu(Menu menu, List<MenuOption> options);
        Menu DeleteMenu(Menu menu);
    }
}
