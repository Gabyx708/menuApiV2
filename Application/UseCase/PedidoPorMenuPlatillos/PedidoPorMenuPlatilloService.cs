//using Application.Interfaces.IPedidoPor;
//using Application.Request.PedidoRequests;
//using Application.Response.PedidoPorMenuOptionResponses;
//using Domain.Entities;

//namespace Application.UseCase.PedidoPorMenuOptions
//{
//    public class PedidoPorMenuOptionService : IPedidoPorMenuOptionService
//    {
//        private readonly IPedidoPorMenuOptionQuery _query;
//        private readonly IPedidoPorMenuOptionCommand _command;

//        public PedidoPorMenuOptionService(IPedidoPorMenuOptionQuery query, IPedidoPorMenuOptionCommand command)
//        {
//            _query = query;
//            _command = command;
//        }

//        public PedidoPorMenuOptionResponse CreatePedidoPorMenuOption(PedidoPorMenuOptionRequest request)
//        {
//            PedidoPorMenuOption nuevoPedidoPorMenuOption = new PedidoPorMenuOption
//            {
//                IdPedido = request.idPedido,
//                IdMenuOption = request.idMenuOption
//            };

//            _command.CreatePedidoPorMenuOption(nuevoPedidoPorMenuOption);
//            return GetPedidoPorMenuOption(nuevoPedidoPorMenuOption.IdPedido, nuevoPedidoPorMenuOption.IdMenuOption);
//        }

//        public PedidoPorMenuOptionResponse DeletePedidoPorMenuOption(Guid idPedido, Guid idMenuPlatilo)
//        {
//            throw new NotImplementedException();
//        }

//        public PedidoPorMenuOptionResponse GetPedidoPorMenuOption(Guid idPedido, Guid idMenuOption)
//        {
//            var found = _query.GetPedidoPorMenuOption(idPedido, idMenuOption);

//            if (found != null)
//            {
//                return new PedidoPorMenuOptionResponse
//                {
//                    IdPedido = idPedido,
//                    IdMenuOption = idMenuOption
//                };
//            }

//            return null;
//        }

//        public List<PedidoPorMenuOptionResponse> GetPedidoPorMenuOptionDeMenu(Guid idMenu)
//        {
//            List<PedidoPorMenuOption> pedidoPorMenuOptions = _query.GetPedidoMenuOptionByMenu(idMenu);
//            List<PedidoPorMenuOptionResponse> pedidoPorMenuOptionResponses = new List<PedidoPorMenuOptionResponse>();

//            foreach (var pedidoPorMenuOption in pedidoPorMenuOptions)
//            {
//                var pedidoPorMenu = GetPedidoPorMenuOption(pedidoPorMenuOption.IdPedido, pedidoPorMenuOption.IdMenuOption);
//                pedidoPorMenuOptionResponses.Add(pedidoPorMenu);
//            }

//            return pedidoPorMenuOptionResponses;
//        }

//        public List<PedidoPorMenuOptionResponse> GetPedidosMenuOptionDePedido(Guid idPedido)
//        {
//            List<PedidoPorMenuOption> pedidoPorMenuOptions = _query.GetPedidoMenuOptionByPedido(idPedido);
//            List<PedidoPorMenuOptionResponse> pedidoPorMenuOptionResponses = new List<PedidoPorMenuOptionResponse>();

//            foreach (var pedidoPorMenuOption in pedidoPorMenuOptions)
//            {
//                var pedidoPorMenu = GetPedidoPorMenuOption(pedidoPorMenuOption.IdPedido, pedidoPorMenuOption.IdMenuOption);
//                pedidoPorMenuOptionResponses.Add(pedidoPorMenu);
//            }

//            return pedidoPorMenuOptionResponses;
//        }

//    }
//}
