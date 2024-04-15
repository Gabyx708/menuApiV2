using Application.Interfaces.IPedidoPorMenuPlatillo;
using Application.Request.PedidoRequests;
using Application.Response.PedidoPorMenuPlatilloResponses;
using Domain.Entities;

namespace Application.UseCase.PedidoPorMenuPlatillos
{
    public class PedidoPorMenuPlatilloService : IPedidoPorMenuPlatilloService
    {
        private readonly IPedidoPorMenuPlatilloQuery _query;
        private readonly IPedidoPorMenuPlatilloCommand _command;

        public PedidoPorMenuPlatilloService(IPedidoPorMenuPlatilloQuery query, IPedidoPorMenuPlatilloCommand command)
        {
            _query = query;
            _command = command;
        }

        public PedidoPorMenuPlatilloResponse CreatePedidoPorMenuPlatillo(PedidoPorMenuPlatilloRequest request)
        {
            PedidoPorMenuPlatillo nuevoPedidoPorMenuPlatillo = new PedidoPorMenuPlatillo
            {
                IdPedido = request.idPedido,
                IdMenuPlatillo = request.idMenuPlatillo
            };

            _command.CreatePedidoPorMenuPlatillo(nuevoPedidoPorMenuPlatillo);
            return GetPedidoPorMenuPlatillo(nuevoPedidoPorMenuPlatillo.IdPedido, nuevoPedidoPorMenuPlatillo.IdMenuPlatillo);
        }

        public PedidoPorMenuPlatilloResponse DeletePedidoPorMenuPlatillo(Guid idPedido, Guid idMenuPlatilo)
        {
            throw new NotImplementedException();
        }

        public PedidoPorMenuPlatilloResponse GetPedidoPorMenuPlatillo(Guid idPedido, Guid idMenuPlatillo)
        {
            var found = _query.GetPedidoPorMenuPlatillo(idPedido, idMenuPlatillo);

            if (found != null)
            {
                return new PedidoPorMenuPlatilloResponse
                {
                    IdPedido = idPedido,
                    IdMenuPlatillo = idMenuPlatillo
                };
            }

            return null;
        }

        public List<PedidoPorMenuPlatilloResponse> GetPedidoPorMenuPlatilloDeMenu(Guid idMenu)
        {
            List<PedidoPorMenuPlatillo> pedidoPorMenuPlatillos = _query.GetPedidoMenuPlatilloByMenu(idMenu);
            List<PedidoPorMenuPlatilloResponse> pedidoPorMenuPlatilloResponses = new List<PedidoPorMenuPlatilloResponse>();

            foreach (var pedidoPorMenuPlatillo in pedidoPorMenuPlatillos)
            {
                var pedidoPorMenu = GetPedidoPorMenuPlatillo(pedidoPorMenuPlatillo.IdPedido, pedidoPorMenuPlatillo.IdMenuPlatillo);
                pedidoPorMenuPlatilloResponses.Add(pedidoPorMenu);
            }

            return pedidoPorMenuPlatilloResponses;
        }

        public List<PedidoPorMenuPlatilloResponse> GetPedidosMenuPlatilloDePedido(Guid idPedido)
        {
            List<PedidoPorMenuPlatillo> pedidoPorMenuPlatillos = _query.GetPedidoMenuPlatilloByPedido(idPedido);
            List<PedidoPorMenuPlatilloResponse> pedidoPorMenuPlatilloResponses = new List<PedidoPorMenuPlatilloResponse>();

            foreach (var pedidoPorMenuPlatillo in pedidoPorMenuPlatillos)
            {
                var pedidoPorMenu = GetPedidoPorMenuPlatillo(pedidoPorMenuPlatillo.IdPedido, pedidoPorMenuPlatillo.IdMenuPlatillo);
                pedidoPorMenuPlatilloResponses.Add(pedidoPorMenu);
            }

            return pedidoPorMenuPlatilloResponses;
        }

    }
}
