using Domain.Entities;

namespace Application.Interfaces.IMenuPlatillo
{
    public interface IMenuPlatilloQuery
    {
        List<MenuPlatillo> GetMenuPlatilloByMenuId(Guid idMenu);
        MenuPlatillo GetById(Guid idMenuPlatillo);
    }
}
