using Domain.Entities;

namespace Application.Interfaces.IMenu
{
    public interface IMenuCommand
    {
        Menu InsertMenu(Menu menu);
        Menu DeleteMenu(Menu menu);
    }
}
