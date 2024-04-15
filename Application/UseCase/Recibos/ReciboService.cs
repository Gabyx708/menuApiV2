using Application.Interfaces.IDescuento;
using Application.Interfaces.IRecibo;
using Application.Response.ReciboResponses;
using Domain.Entities;

namespace Application.UseCase.Recibos
{
    public class ReciboService : IReciboService
    {
        private readonly IReciboCommand _command;
        private readonly IReciboQuery _query;
        private readonly IDescuentoService _descuentoService;

        public ReciboService(IReciboCommand command, IReciboQuery query, IDescuentoService descuentoService)
        {
            _command = command;
            _query = query;
            _descuentoService = descuentoService;
        }


        public ReciboResponse CrearRecibo()
        {
            Recibo nuevoRecibo = new Recibo
            {
                precioTotal = 0,
                IdDescuento = _descuentoService.GetDescuentoVigente().id
            };

            var recibo = _command.CrearRecibo(nuevoRecibo);

            return GetReciboById(recibo.IdRecibo);
        }

        public ReciboResponse GetReciboById(Guid id)
        {
            var recibo = _query.GetById(id);

            if (recibo != null)
            {
                return new ReciboResponse
                {
                    id = recibo.IdRecibo,
                    descuento = _descuentoService.GetDescuentoById(recibo.IdDescuento).porcentaje,
                    precio = recibo.precioTotal
                };
            }

            return null;
        }

        public List<ReciboResponse> GetRecibosByDescuento(Guid idDescuento)
        {
            List<ReciboResponse> recibosResponses = new List<ReciboResponse>();

            var recibosProcesar = _query.GetAllByDescuento(idDescuento);

            foreach (var recibo in recibosProcesar)
            {
                ReciboResponse reciboResponse = GetReciboById(recibo.IdRecibo);
                recibosResponses.Add(reciboResponse);
            }

            return recibosResponses;
        }

        public List<ReciboResponse> GetRecibosPersonal(Guid idPersonal)
        {
            List<ReciboResponse> recibosResponses = new List<ReciboResponse>();

            var recibosProcesar = _query.GetAllByPersonal(idPersonal);

            foreach (var recibo in recibosProcesar)
            {
                ReciboResponse reciboResponse = GetReciboById(recibo.IdRecibo);
                recibosResponses.Add(reciboResponse);
            }

            return recibosResponses;
        }

        public ReciboResponse CambiarPrecio(Guid idRecibo, decimal precioTotal)
        {
            var recibo = _command.CambiarPrecioTotal(idRecibo, precioTotal);
            return GetReciboById(recibo.IdRecibo);
        }
    }
}
