using Domain.Entities;

namespace Application.Interfaces.IRecibo
{
    public interface IReciboQuery
    {
        Recibo GetById(Guid id);
        List<Recibo> GetAllByPersonal(Guid idPersonal);
        List<Recibo> GetAll();
        List<Recibo> GetAllByDescuento(Guid idDescuento);
    }
}
