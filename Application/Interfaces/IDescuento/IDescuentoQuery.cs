using Domain.Entities;

namespace Application.Interfaces.IDescuento
{
    public interface IDescuentoQuery
    {
        Discount GetById(Guid idDescuento);
        Discount GetByFecha(DateTime fecha);
        List<Discount> GetAll();
        Discount GetVigente();

    }
}
