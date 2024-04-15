using Application.Request.DescuestoRequests;
using Application.Response.DescuentoResponse;

namespace Application.Interfaces.IDescuento
{
    public interface IDescuentoService
    {
        DescuentoResponse crearDescuento(DescuentoRequest request);
        DescuentoResponse GetDescuentoById(Guid id);
        DescuentoResponse GetDescuentoVigente();
        DescuentoResponse GetByFecha(DateTime fecha);
        List<DescuentoResponse> GetAll();
    }
}
