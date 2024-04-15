using Application.Exceptions;
using Application.Interfaces.IAutorizacionPedido;
using Application.Interfaces.IMenu;
using Application.Interfaces.IMenuPlatillo;
using Application.Interfaces.IPedido;
using Application.Interfaces.IPedidoPorMenuPlatillo;
using Application.Interfaces.IPersonal;
using Application.Interfaces.IPlatillo;
using Application.Interfaces.IRecibo;
using Application.Request.MenuPlatilloRequests;
using Application.Request.PedidoRequests;
using Application.Response.AutorizacionPedidoResponses;
using Application.Response.MenuPlatilloResponses;
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
        private readonly IPedidoPorMenuPlatilloService _pedidoPorMenuPlatilloService;
        private readonly IMenuPlatilloService _menuPlatilloService;
        private readonly IPlatilloService _platilloService;
        private readonly IReciboService _reciboService;
        private readonly IReciboCommand _reciboCommand;
        private readonly IMenuPlatilloQuery _menuPlatilloQuery;
        private readonly IMenuService _menuService;
        private readonly IRepositoryAutorizacionPedido _repositoryAuthPedido;
        public PedidoService(IPedidoCommand command, IPedidoQuery query, IPersonalService personalService, IPedidoPorMenuPlatilloService pedidoPorMenuPlatilloService, IMenuPlatilloService menuPlatilloService, IPlatilloService platilloService, IReciboService reciboService, IMenuPlatilloQuery menuPlatilloQuery, IMenuService menuService, IReciboCommand reciboCommand, IRepositoryAutorizacionPedido repositoryAuthPedido)
        {
            _command = command;
            _query = query;
            _personalService = personalService;
            _pedidoPorMenuPlatilloService = pedidoPorMenuPlatilloService;
            _menuPlatilloService = menuPlatilloService;
            _platilloService = platilloService;
            _reciboService = reciboService;
            _menuPlatilloQuery = menuPlatilloQuery;
            _menuService = menuService;
            _reciboCommand = reciboCommand;
            _repositoryAuthPedido = repositoryAuthPedido;
        }

        public PedidoResponse GetPedidoById(Guid idPedido)
        {
            var pedido = _query.GetPedidoById(idPedido);
            List<MenuPlatilloGetResponse> platillosDelMenu = new List<MenuPlatilloGetResponse>();

            if (pedido != null)
            {
                var pedidosPorMenuPlatillo = _pedidoPorMenuPlatilloService.GetPedidosMenuPlatilloDePedido(idPedido);

                foreach (var pedidoPorMenuPlatillo in pedidosPorMenuPlatillo)
                {
                    var recuperado = _menuPlatilloService.GetMenuPlatilloById(pedidoPorMenuPlatillo.IdMenuPlatillo);

                    MenuPlatilloGetResponse menuPlatilloResponse = new MenuPlatilloGetResponse
                    {
                        id = recuperado.id,
                        descripcion = _platilloService.GetPlatilloById(recuperado.id).descripcion,
                        precio = recuperado.precio,
                        stock = recuperado.stock,
                        pedido = recuperado.pedido,
                    };

                    platillosDelMenu.Add(menuPlatilloResponse);
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
                Guid idMenuPlatilloPrimero = found.PedidosPorMenuPlatillo[0].IdMenuPlatillo;
                DateTime fechaCierreMenu = _menuPlatilloQuery.GetById(idMenuPlatilloPrimero).Menu.FechaCierre;
                DateTime fechaActual = DateTime.Now;

                if (fechaActual > fechaCierreMenu)
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
            if (request.MenuPlatillos.Count < 1 || request.MenuPlatillos.Count == 0)
            {
                throw new SystemExceptionApp("pedido sin platillos", 400);
            }

            List<PedidoGetResponse> existePedido = PedidoFiltrado(request.idUsuario, DateTime.Now.Date, DateTime.Now.Date, 1);
            var fechaActual = DateTime.Now;

            MenuResponse ultimoMenu = _menuService.GetUltimoMenu();

            var fechaCierreMenu = ultimoMenu.fecha_cierre;
            var fechaCargaMenu = ultimoMenu.fecha_carga;

            if (existePedido.Count > 0)
            {
                throw new SystemExceptionApp("pedido ya hecho", 409);
            }

            if (fechaActual < fechaCargaMenu)
            {
                throw new SystemExceptionApp("fecha menor a fecha carga", 409);
            }

            if (fechaActual > fechaCierreMenu)
            {
                throw new SystemExceptionApp("fecha mayor a fecha cierre", 409);
            }


            decimal precioTotal = 0;

            Pedido nuevoPedido = new Pedido
            {
                IdPersonal = request.idUsuario,
                FechaDePedido = DateTime.Now,
                IdRecibo = _reciboService.CrearRecibo().id
            };

            _command.createPedido(nuevoPedido);
            Logger.LogInformation("create new order: {@order} for user: {@user}",
                                  nuevoPedido.IdPedido, nuevoPedido.IdPersonal);

            foreach (var menuPlatilloId in request.MenuPlatillos)
            {
                var menuPlatilloEcontrado = _menuPlatilloService.GetMenuPlatilloById(menuPlatilloId);
                decimal precioPlatillo = menuPlatilloEcontrado.precio;
                precioTotal = precioTotal + precioPlatillo;

                PedidoPorMenuPlatilloRequest requestPedidoPorMenuPlatillo = new PedidoPorMenuPlatilloRequest
                {
                    idPedido = nuevoPedido.IdPedido,
                    idMenuPlatillo = menuPlatilloId,
                };

                if (menuPlatilloEcontrado.stock == _menuPlatilloQuery.GetById(menuPlatilloId).Solicitados)
                {
                    Logger.LogInformation("delete new orer: {@order}", nuevoPedido);
                    return EliminarPedido(nuevoPedido.IdPedido);
                }

                MenuPlatilloRequest modificacion = new MenuPlatilloRequest
                {
                    stock = menuPlatilloEcontrado.stock,
                    solicitados = _menuPlatilloQuery.GetById(menuPlatilloId).Solicitados + 1
                };

                _menuPlatilloService.ModificarMenuPlatillo(menuPlatilloId, modificacion);
                _pedidoPorMenuPlatilloService.CreatePedidoPorMenuPlatillo(requestPedidoPorMenuPlatillo);
            }

            _reciboService.CambiarPrecio(nuevoPedido.IdRecibo, precioTotal);

            return GetPedidoById(nuevoPedido.IdPedido);
        }

        public PedidoResponse HacerUnpedidoSinRestricciones(PedidoRequest request, Guid usuarioPedidor)
        {
            if (request.MenuPlatillos.Count < 1 || request.MenuPlatillos.Count == 0)
            {
                throw new SystemExceptionApp("pedido sin platillos", 400);
            }

            if (request.MenuPlatillos.GroupBy(mp => mp).Any(g => g.Count() > 1))
            {
                throw new SystemExceptionApp("platillos repetidos", 400);
            }

            decimal precioTotal = 0;

            Pedido nuevoPedido = new Pedido
            {
                IdPersonal = request.idUsuario,
                FechaDePedido = DateTime.Now,
                IdRecibo = _reciboService.CrearRecibo().id
            };

            _command.createPedido(nuevoPedido);
            Logger.LogInformation("create new order without restrictions: order:{@order} user: {@user}"
                                  , nuevoPedido.IdPedido, nuevoPedido.IdPersonal);

            foreach (var menuPlatilloId in request.MenuPlatillos)
            {
                var menuPlatilloEcontrado = _menuPlatilloService.GetMenuPlatilloById(menuPlatilloId);
                decimal precioPlatillo = menuPlatilloEcontrado.precio;
                precioTotal = precioTotal + precioPlatillo;

                PedidoPorMenuPlatilloRequest requestPedidoPorMenuPlatillo = new PedidoPorMenuPlatilloRequest
                {
                    idPedido = nuevoPedido.IdPedido,
                    idMenuPlatillo = menuPlatilloId,
                };

                if (menuPlatilloEcontrado.stock == _menuPlatilloQuery.GetById(menuPlatilloId).Solicitados)
                {
                    return EliminarPedido(nuevoPedido.IdPedido);
                }

                MenuPlatilloRequest modificacion = new MenuPlatilloRequest
                {
                    stock = menuPlatilloEcontrado.stock,
                    solicitados = _menuPlatilloQuery.GetById(menuPlatilloId).Solicitados + 1
                };

                _menuPlatilloService.ModificarMenuPlatillo(menuPlatilloId, modificacion);
                _pedidoPorMenuPlatilloService.CreatePedidoPorMenuPlatillo(requestPedidoPorMenuPlatillo);
            }

            _reciboService.CambiarPrecio(nuevoPedido.IdRecibo, precioTotal);

            var autorizacion = new AutorizacionPedido
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

            List<Pedido> pedidos = _query.GetPedidosFiltrado(idPersonal, Desde, Hasta, cantidad);
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
