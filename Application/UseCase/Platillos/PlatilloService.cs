using Application.Interfaces.IPlatillo;
using Application.Request.PlatilloRequests;
using Application.Response.PlatilloResponses;
using Domain.Entities;

namespace Application.UseCase.Platillos
{
    public class PlatilloService : IPlatilloService
    {
        private readonly IPlatilloQuery _query;
        private readonly IPlatilloCommand _command;

        public PlatilloService(IPlatilloQuery query, IPlatilloCommand command)
        {
            _query = query;
            _command = command;
        }

        public PlatilloResponse CreatePlatillo(PlatilloRequest request)
        {
            var nuevoPlatillo = new Platillo
            {
                Descripcion = request.descripcion,
                Precio = request.precio,
                Activado = true
            };

            _command.CreatePlatillo(nuevoPlatillo);
            var responsePlatillo = GetPlatilloById(nuevoPlatillo.IdPlatillo);
            return responsePlatillo;
        }

        public List<PlatilloResponse> GetAll()
        {
            var platillosAMapear = _query.GetAll();
            List<PlatilloResponse> platillos = new List<PlatilloResponse>();

            foreach (var platilloMapear in platillosAMapear)
            {
                var platoAdd = new PlatilloResponse
                {
                    id = platilloMapear.IdPlatillo,
                    descripcion = platilloMapear.Descripcion,
                    precio = platilloMapear.Precio,
                    activado = platilloMapear.Activado
                };

                platillos.Add(platoAdd);
            }

            return platillos;
        }

        public PlatilloResponse GetPlatilloById(int idPlatillo)
        {
            var platilloRecuperado = _query.GetPlatilloById(idPlatillo);

            if (platilloRecuperado == null) { return null; };

            return new PlatilloResponse
            {
                id = platilloRecuperado.IdPlatillo,
                descripcion = platilloRecuperado.Descripcion,
                precio = platilloRecuperado.Precio,
                activado = platilloRecuperado.Activado
            };
        }

        public PlatilloResponse UpdatePrecio(int idPlatillo, decimal nuevoPrecio)
        {
            var platilloActualizado = _command.UpdatePrecio(idPlatillo, nuevoPrecio);

            return new PlatilloResponse
            {
                id = platilloActualizado.IdPlatillo,
                descripcion = platilloActualizado.Descripcion,
                precio = platilloActualizado.Precio,
                activado = platilloActualizado.Activado
            };
        }

        public bool AlterarPreciosMasivamente(decimal nuevoPrecio)
        {

            List<PlatilloResponse> todosLosPlatillos = GetAll();

            foreach (var plato in todosLosPlatillos)
            {
                try
                {
                    UpdatePrecio(plato.id, nuevoPrecio);
                }
                catch (Exception e)
                {
                    return false;
                }

            }

            return true;
        }
    }
}
