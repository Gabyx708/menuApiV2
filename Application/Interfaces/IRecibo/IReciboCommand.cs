using Domain.Entities;

namespace Application.Interfaces.IRecibo
{
    public interface IReciboCommand
    {
        Receipt CrearRecibo(Receipt ReciboNuevo);

        Receipt EliminarRecibo(Guid idRecibo);

        Receipt CambiarPrecioTotal(Guid idRecibo, decimal precioTotal);
    }
}
