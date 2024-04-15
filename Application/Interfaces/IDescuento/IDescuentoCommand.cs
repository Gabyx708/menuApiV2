using Domain.Entities;

namespace Application.Interfaces.IDescuento
{
    public interface IDescuentoCommand
    {
        Descuento createDescuento(Descuento descuento);

    }
}
