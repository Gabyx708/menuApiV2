using Domain.Entities;

namespace Application.Interfaces.IRecibo
{
    public interface IReciboCommand
    {
        Recibo CrearRecibo(Recibo ReciboNuevo);

        Recibo EliminarRecibo(Guid idRecibo);

        Recibo CambiarPrecioTotal(Guid idRecibo, decimal precioTotal);
    }
}
