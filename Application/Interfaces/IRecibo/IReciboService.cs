using Application.Response.ReciboResponses;

namespace Application.Interfaces.IRecibo
{
    public interface IReciboService
    {
        ReciboResponse CambiarPrecio(Guid idRecibo, decimal precioTotal);
        ReciboResponse CrearRecibo();
        ReciboResponse GetReciboById(Guid id);
        List<ReciboResponse> GetRecibosPersonal(Guid idPersonal);
        List<ReciboResponse> GetRecibosByDescuento(Guid idDescuento);
    }
}
