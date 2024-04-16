using Domain.Entities;

namespace Application.Interfaces.IPlatillo
{
    public interface IPlatilloQuery
    {
        Dish GetPlatilloById(int id);

        List<Dish> GetAll();

    }
}
