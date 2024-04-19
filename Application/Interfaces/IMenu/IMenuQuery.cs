using Domain.Entities;

namespace Application.Interfaces.IMenu
{
    public interface IMenuQuery
    {
        Menu GetMenuById(Guid idMenu);

        Menu GetNextMenu();

    }
}
