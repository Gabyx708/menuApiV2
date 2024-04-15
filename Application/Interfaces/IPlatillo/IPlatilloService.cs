using Application.Request.PlatilloRequests;
using Application.Response.PlatilloResponses;

namespace Application.Interfaces.IPlatillo
{
    public interface IPlatilloService
    {
        PlatilloResponse GetPlatilloById(int idPlatillo);
        PlatilloResponse CreatePlatillo(PlatilloRequest request);
        PlatilloResponse UpdatePrecio(int idPlatillo, decimal nuevoPrecio);
        List<PlatilloResponse> GetAll();

        bool AlterarPreciosMasivamente(decimal precio);
    }
}
