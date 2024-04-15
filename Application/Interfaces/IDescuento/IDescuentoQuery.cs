using Domain.Entities;

namespace Application.Interfaces.IDescuento
{
    public interface IDescuentoQuery
    {
        Descuento GetById(Guid idDescuento);
        Descuento GetByFecha(DateTime fecha);
        List<Descuento> GetAll();
        Descuento GetVigente();

    }
}
