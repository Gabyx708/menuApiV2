using Application.Interfaces.IDescuento;
using Application.Request.DescuestoRequests;
using Application.Response.DescuentoResponse;
using Domain.Entities;

namespace Application.UseCase.Descuentos
{
    public class DescuentoService : IDescuentoService
    {
        private readonly IDescuentoQuery _query;
        private readonly IDescuentoCommand _command;

        public DescuentoService(IDescuentoQuery query, IDescuentoCommand command)
        {
            _query = query;
            _command = command;
        }

        public DescuentoResponse crearDescuento(DescuentoRequest request)
        {
            var nuevoDescuento = new Descuento
            {
                FechaInicioVigencia = request.fecha_inicio,
                Porcentaje = request.porcentaje
            };

            _command.createDescuento(nuevoDescuento);
            return GetDescuentoById(nuevoDescuento.IdDescuento);
        }

        public List<DescuentoResponse> GetAll()
        {
            var descuentosAMapear = _query.GetAll();
            List<DescuentoResponse> descuentos = new List<DescuentoResponse>();

            foreach (var descuento in descuentosAMapear)
            {
                DescuentoResponse descuentoResponse = GetDescuentoById(descuento.IdDescuento);
                descuentos.Add(descuentoResponse);
            }

            return descuentos;
        }

        public DescuentoResponse GetByFecha(DateTime fecha)
        {
            var descuento = _query.GetByFecha(fecha);
            return GetDescuentoById(descuento.IdDescuento);
        }

        public DescuentoResponse GetDescuentoById(Guid id)
        {
            var descuento = _query.GetById(id);

            if (descuento == null) { return null; };

            return new DescuentoResponse
            {
                id = descuento.IdDescuento,
                fecha_inicio = descuento.FechaInicioVigencia,
                porcentaje = descuento.Porcentaje
            };
        }

        public DescuentoResponse GetDescuentoVigente()
        {
            var vigente = _query.GetVigente();

            if (vigente == null)
            {
                return null;
            }

            return GetDescuentoById(vigente.IdDescuento);
        }
    }
}
