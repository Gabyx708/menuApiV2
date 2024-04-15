using Domain.Entities;

namespace Application.Interfaces.IPlatillo
{
    public interface IPlatilloCommand
    {
        Platillo CreatePlatillo(Platillo platillo);

        Platillo UpdatePrecio(int idPlatillo, decimal nuevoPrecio);
    }
}
