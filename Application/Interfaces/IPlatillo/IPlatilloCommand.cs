using Domain.Entities;

namespace Application.Interfaces.IPlatillo
{
    public interface IPlatilloCommand
    {
        Dish CreatePlatillo(Dish platillo);

        Dish UpdatePrecio(int idPlatillo, decimal nuevoPrecio);
    }
}
