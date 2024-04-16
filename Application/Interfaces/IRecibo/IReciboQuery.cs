using Domain.Entities;

namespace Application.Interfaces.IRecibo
{
    public interface IReciboQuery
    {
        Receipt GetById(Guid id);
        List<Receipt> GetAllByPersonal(Guid idPersonal);
        List<Receipt> GetAll();
        List<Receipt> GetAllByDescuento(Guid idDescuento);
    }
}
