using Domain.Entities;

namespace Application.Interfaces.IPlatillo
{
    public interface IPlatilloQuery
    {
        Platillo GetPlatilloById(int id);

        List<Platillo> GetAll();

    }
}
