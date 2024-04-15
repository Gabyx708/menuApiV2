using Domain.Entities;

namespace Application.Interfaces.IPagos
{
    public interface IPagoQuery
    {
        List<Pago> GetAllPagos();
        Pago GetPagoById(long NPago);
        List<Pago> GetPagoFiltrado(DateTime fechaDesde, DateTime fechaHasta);
    }
}
