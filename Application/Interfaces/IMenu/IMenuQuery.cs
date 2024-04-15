using Domain.Entities;

namespace Application.Interfaces.IMenu
{
    public interface IMenuQuery
    {
        Menu GetMenuById(Guid idMenu);

        List<MenuPlatillo> PlatillosDelMenu(Guid idMenu);
        Menu GetByDateConsumo(DateTime fechaConsumo);
        Menu GetUltimoMenu();


    }
}
