using Application.Exceptions;
using Application.Interfaces.IAutorizacionPedido;
using Application.Interfaces.IMenu;
using Application.Interfaces.IMenuOption;
using Application.Interfaces.IPedido;
using Application.Interfaces.IPedidoPorMenuOption;
using Application.Interfaces.IPersonal;
using Application.Interfaces.IPlatillo;
using Application.Interfaces.IRecibo;
using Application.Request.MenuOptionRequests;
using Application.Request.PedidoRequests;
using Application.Response.AutorizacionPedidoResponses;
using Application.Response.MenuOptionResponses;
using Application.Response.MenuResponses;
using Application.Response.PedidoResponses;
using Application.Response.PersonalResponses;
using Application.Tools.Log;
using Domain.Entities;

namespace Application.UseCase.Pedidos
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoCommand _command;
        private readonly IPedidoQuery _query;
        private readonly IPersonalService _personalService;
        private readonly IPedidoPorMenuOptionService _pedidoPorMenuOptionService;
        private readonly IMenuOptionService _MenuOptionService;
        private readonly IPlatilloService _platilloService;
        private readonly IReciboService _reciboService;
        private readonly IReciboCommand _reciboCommand;
        private readonly IMenuOptionQuery _MenuOptionQuery;
        private readonly IMenuService _menuService;
        private readonly IRepositoryAutorizacionPedido _repositoryAuthPedido;
        public PedidoService(IPedidoCommand command, IPedidoQuery query, IPersonalService personalService, IPedidoPorMenuOptionService pedidoPorMenuOptionService, IMenuOptionService MenuOptionService, IPlatilloService platilloService, IReciboService reciboService, IMenuOptionQuery MenuOptionQuery, IMenuService menuService, IReciboCommand reciboCommand, IRepositoryAutorizacionPedido repositoryAuthPedido)
        {
            _command = command;
            _query = query;
            _personalService = personalService;
            _pedidoPorMenuOptionService = pedidoPorMenuOptionService;
            _MenuOptionService = MenuOptionService;
            _platilloService = platilloService;
            _reciboService = reciboService;
            _MenuOptionQuery = MenuOptionQuery;
            _menuService = menuService;
            _reciboCommand = reciboCommand;
            _repositoryAuthPedido = repositoryAuthPedido;
        }

        public PedidoResponse GetPedidoById(Guid idPedido)
        {
            var pedido = _query.GetPedidoById(idPedido);
            List<MenuOptionGetResponse> platillosDelMenu = new List<MenuOptionGetResponse>();

            if (pedido != null)
            {
                var pedidosPorMenuOption = _pedidoPorMenuOptionService.GetPedidosMenuOptionDePedido(idPedido);

                foreach (var pedidoPorMenuOption in pedidosPorMenuOption)
                {
                    var recuperado = _MenuOptionService.GetMenuOptionById(pedidoPorMenuOption.IdMenuOption);

                    MenuOptionGetResponse MenuOptionResponse = new MenuOptionGetResponse
                    {
                        id = recuperado.id,
                        descripcion = _platilloService.GetPlatilloById(recuperado.id).descripcion,
                        precio = recuperado.precio,
                        stock = recuperado.stock,
                        pedido = recuperado.pedido,
                    };

                    platillosDelMenu.Add(MenuOptionResponse);
                }

                var personal = _personalService.GetPersonalById(pedido.IdPersonal);
                var autorizaicion = _repositoryAuthPedido.GetAutorizacionPedidoByidPedido(pedido.IdPedido);

                AutorizacionPedidoResponse authResponse = null;

                if (autorizaicion != null)
                {
                    var personalAutorizador = _personalService.GetPersonalById(autorizaicion.IdPersonal);

                    authResponse = new AutorizacionPedidoResponse
                    {
                        idPedido = autorizaicion.IdPedido,
                        Autorizador = personalAutorizador.id,
                        Nombre = personalAutorizador.nombre + " " + personalAutorizador.apellido
                    };
                }

                return new PedidoResponse
                {
                    idPedido = idPedido,
                    Nombre = personal.nombre + " " + personal.apellido,
                    fecha = pedido.FechaDePedido,
                    platillos = platillosDelMenu,
                    recibo = _reciboService.GetReciboById(pedido.IdRecibo),
                    Autorizacion = authResponse
                };
            }

            return null;
        }

        public PedidoResponse EliminarPedido(Guid idPedido)
        {
            var found = _query.GetPedidoById(idPedido);
            var auxResponse = GetPedidoById(idPedido);

            if (found != null)
            {
                Guid idMenuOptionPrimero = found.PedidosPorMenuOption[0].IdMenuOption;
                DateTime CloseDateMenu = _MenuOptionQuery.GetById(idMenuOptionPrimero).Menu.CloseDate;
                DateTime fechaActual = DateTime.Now;

                if (fechaActual > CloseDateMenu)
                {
                    throw new InvalidOperationException();
                }

                _command.DeletePedido(idPedido);
                return auxResponse;
            }

            return auxResponse;
        }



        public PedidoResponse HacerUnpedido(PedidoRequest request)
        {
            if (request.MenuOptions.Count < 1 || request.MenuOptions.Count == 0)
            {
                throw new SystemExceptionApp("pedido sin platillos", 400);
            }

            List<PedidoGetResponse> existePedido = PedidoFiltrado(request.idUsuario, DateTime.Now.Date, DateTime.Now.Date, 1);
            var fechaActual = DateTime.Now;

            MenuResponse ultimoMenu = _menuService.GetUltimoMenu();

            var CloseDateMenu = ultimoMenu.fecha_cierre;
            var UploadDateMenu = ultimoMenu.fecha_carga;

            if (existePedido.Count > 0)
            {
                throw new SystemExceptionApp("pedido ya hecho", 409);
            }

            if (fechaActual < UploadDateMenu)
            {
                throw new SystemExceptionApp("fecha menor a fecha carga", 409);
            }

            if (fechaActual > CloseDateMenu)
            {
                throw new SystemExceptionApp("fecha mayor a fecha cierre", 409);
            }


            decimal precioTotal = 0;

            Order nuevoPedido = new Order
            {
                IdPersonal = request.idUsuario,
                FechaDePedido = DateTime.Now,
                IdRecibo = _reciboService.CrearRecibo().id
            };

            _command.createPedido(nuevoPedido);
            Logger.LogInformation("create new order: {@order} for user: {@user}",
                                  nuevoPedido.IdPedido, nuevoPedido.IdPersonal);

            foreach (var MenuOptionId in request.MenuOptions)
            {
                var MenuOptionEcontrado = _MenuOptionService.GetMenuOptionById(MenuOptionId);
                decimal precioPlatillo = MenuOptionEcontrado.precio;
                precioTotal = precioTotal + precioPlatillo;

                PedidoPorMenuOptionRequest requestPedidoPorMenuOption = new PedidoPorMenuOptionRequest
                {
                    idPedido = nuevoPedido.IdPedido,
                    idMenuOption = MenuOptionId,
                };

                if (MenuOptionEcontrado.stock == _MenuOptionQuery.GetById(MenuOptionId).Solicitados)
                {
                    Logger.LogInformation("delete new orer: {@order}", nuevoPedido);
                    return EliminarPedido(nuevoPedido.IdPedido);
                }

                MenuOptionRequest modificacion = new MenuOptionRequest
                {
                    stock = MenuOptionEcontrado.stock,
                    solicitados = _MenuOptionQuery.GetById(MenuOptionId).Solicitados + 1
                };

                _MenuOptionService.ModificarMenuOption(MenuOptionId, modificacion);
                _pedidoPorMenuOptionService.CreatePedidoPorMenuOption(requestPedidoPorMenuOption);
            }

            _reciboService.CambiarPrecio(nuevoPedido.IdRecibo, precioTotal);

            return GetPedidoById(nuevoPedido.IdPedido);
        }

        public PedidoResponse HacerUnpedidoSinRestricciones(PedidoRequest request, Guid usuarioPedidor)
        {
            if (request.MenuOptions.Count < 1 || request.MenuOptions.Count == 0)
            {
                throw new SystemExceptionApp("pedido sin platillos", 400);
            }

            if (request.MenuOptions.GroupBy(mp => mp).Any(g => g.Count() > 1))
            {
                throw new SystemExceptionApp("platillos repetidos", 400);
            }

            decimal precioTotal = 0;

            Order nuevoPedido = new Order
            {
                IdPersonal = request.idUsuario,
                FechaDePedido = DateTime.Now,
                IdRecibo = _reciboService.CrearRecibo().id
            };

            _command.createPedido(nuevoPedido);
            Logger.LogInformation("create new order without restrictions: order:{@order} user: {@user}"
                                  , nuevoPedido.IdPedido, nuevoPedido.IdPersonal);

            foreach (var MenuOptionId in request.MenuOptions)
            {
                var MenuOptionEcontrado = _MenuOptionService.GetMenuOptionById(MenuOptionId);
                decimal precioPlatillo = MenuOptionEcontrado.precio;
                precioTotal = precioTotal + precioPlatillo;

                PedidoPorMenuOptionRequest requestPedidoPorMenuOption = new PedidoPorMenuOptionRequest
                {
                    idPedido = nuevoPedido.IdPedido,
                    idMenuOption = MenuOptionId,
                };

                if (MenuOptionEcontrado.stock == _MenuOptionQuery.GetById(MenuOptionId).Solicitados)
                {
                    return EliminarPedido(nuevoPedido.IdPedido);
                }

                MenuOptionRequest modificacion = new MenuOptionRequest
                {
                    stock = MenuOptionEcontrado.stock,
                    solicitados = _MenuOptionQuery.GetById(MenuOptionId).Solicitados + 1
                };

                _MenuOptionService.ModificarMenuOption(MenuOptionId, modificacion);
                _pedidoPorMenuOptionService.CreatePedidoPorMenuOption(requestPedidoPorMenuOption);
            }

            _reciboService.CambiarPrecio(nuevoPedido.IdRecibo, precioTotal);

            var autorizacion = new Authorization
            {
                IdPersonal = usuarioPedidor,
                IdPedido = nuevoPedido.IdPedido
            };

            _repositoryAuthPedido.CreateAutorizacionPedido(autorizacion);
            Logger.LogInformation("create new authorization order:{@auth} by user {@user}",
                                   autorizacion.IdPedido, autorizacion.IdPersonal);

            return GetPedidoById(nuevoPedido.IdPedido);
        }




        public List<PedidoResponse> PedidosDelMenu(Guid idMenu)
        {
            throw new NotImplementedException();
        }

        public List<PedidoGetResponse> PedidoFiltrado(Guid? idPersonal, DateTime? Desde, DateTime? Hasta, int? cantidad)
        {

            List<Order> pedidos = _query.GetPedidosFiltrado(idPersonal, Desde, Hasta, cantidad);
            List<PedidoGetResponse> pedidosResponse = new List<PedidoGetResponse>();


            foreach (var pedido in pedidos)
            {
                PersonalResponse persona = _personalService.GetPersonalById(pedido.IdPersonal);


                var nuevo = new PedidoGetResponse
                {
                    id = pedido.IdPedido,
                    Personal = pedido.IdPersonal,
                    Fecha = pedido.FechaDePedido,
                    Recibo = pedido.IdRecibo,
                    Nombre = persona.nombre + " " + persona.apellido
                };

                pedidosResponse.Add(nuevo);
            }

            return pedidosResponse;
        }

        public List<PedidoResponse> PedidosPorFecha(DateTime fecha)
        {
            throw new NotImplementedException();
        }
    }
}
