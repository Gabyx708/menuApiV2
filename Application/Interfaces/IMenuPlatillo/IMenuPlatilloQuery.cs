using Domain.Entities;

namespace Application.Interfaces.I

{
    public interface IMenuOptionQuery
    {
        List<MenuOption> GetMenuOptionByMenuId(Guid idMenu);
        MenuOption GetById(Guid idMenuOption);
    }
}
