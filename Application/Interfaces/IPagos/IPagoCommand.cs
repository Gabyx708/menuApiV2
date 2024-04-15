using Domain.Entities;

namespace Application.Interfaces.IPagos
{
    public interface IPagoCommand
    {
        Pago InsertPago(Pago NuevoPago, List<Guid> idRecibos);
        Pago ModificarEstadoAnulado(long NPago, bool estadoAnulado);
    }
}
