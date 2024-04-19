using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IMenu
{
    public interface IMenuQuery
    {
        Menu GetMenuById(Guid idMenu);
        Menu GetNextAvailableMenu();
        PaginatedList<Menu> GetMenuList(DateTime? InitialDate, DateTime? FinalDate, int index, int quantity);
        PaginatedList<Menu> GetAll(int index, int quantity);
    }
}
