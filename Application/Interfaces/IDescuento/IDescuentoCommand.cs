using Domain.Entities;

namespace Application.Interfaces.IDescuento
{
    public interface IDescuentoCommand
    {
        Discount createDescuento(Discount descuento);

    }
}
