using Application.Request.PagoRequests;
using Application.Response.PagoResponses;

namespace Application.Interfaces.IPagos
{
    public interface IPagoService
    {
        PagoResponse GetPagoResponseById(long id);
        PagoResponse HacerUnPago(PagoRequest request);
        PagoResponse ModificarAnulacion(long NumeroPago, bool IsAnulado);
        List<PagoResponse> ObtenerPagosFiltrados(DateTime fechaDesde, DateTime fechaHasta);
        List<PagoResponse> GetAll();
    }
}
