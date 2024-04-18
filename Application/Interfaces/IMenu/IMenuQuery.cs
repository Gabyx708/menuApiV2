using Domain.Entities;

namespace Application.Interfaces.IMenu
{
    public interface IMenuQuery
    {
        Menu GetMenuById(Guid idMenu);

        List<
            > PlatillosDelMenu(Guid idMenu);
        Menu GetByDateConsumo(DateTime EatingDate);
        Menu GetUltimoMenu();


    }
}
