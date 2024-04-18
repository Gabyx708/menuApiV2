using Application.Interfaces.IMenuOption;
using Application.Interfaces.IPlatillo;
using Application.Request.MenuOptionRequests;
using Application.Response.MenuOptionResponses;
using Domain.Entities;

namespace Application.UseCase.MenuOptions
{
    public class MenuOptionService : IMenuOptionService
    {
        private readonly IMenuOptionQuery _query;
        private readonly IMenuOptionCommand _command;
        private readonly IPlatilloService _platilloService;

        public MenuOptionService(IMenuOptionQuery query, IMenuOptionCommand command, IPlatilloService platilloService)
        {
            _query = query;
            _command = command;
            _platilloService = platilloService;
        }

        public List<MenuOptionResponse> AsignarPlatillosAMenu(Guid idMenu, List<MenuOptionRequest> platillos)
        {
            foreach (var platillo in platillos)
            {
                _command.AsignarPlatilloAMenu(idMenu, platillo.id, platillo.stock);
            }

            var platillosDelMenu = _query.GetMenuOptionByMenuId(idMenu);
            List<MenuOptionResponse> MenuOptions = new List<MenuOptionResponse>();

            foreach (var platilloMapear in platillosDelMenu)
            {
                var platilloResponse = new MenuOptionResponse
                {
                    id = platilloMapear.IdPlatillo,
                    precio = platilloMapear.PrecioActual,
                    stock = platilloMapear.Stock,
                    pedido = platilloMapear.Solicitados
                };

                MenuOptions.Add(platilloResponse);
            }

            return MenuOptions;
        }

        public MenuOptionResponse GetMenuOptionById(Guid id)
        {
            var MenuOption = _query.GetById(id);

            return new MenuOptionResponse
            {
                id = MenuOption.IdPlatillo,
                precio = MenuOption.PrecioActual,
                stock = MenuOption.Stock,
                pedido = MenuOption.Solicitados
            };
        }

        public List<MenuOptionGetResponse> GetMenuOptionDelMenu(Guid idMenu)
        {
            var platillosDelMenu = _query.GetMenuOptionByMenuId(idMenu);
            List<MenuOptionGetResponse> MenuOptions = new List<MenuOptionGetResponse>();

            foreach (var plato in platillosDelMenu)
            {
                var response = new MenuOptionGetResponse
                {
                    id = plato.IdPlatillo,
                    idMenuPlato = plato.IdMenuOption,
                    descripcion = _platilloService.GetPlatilloById(plato.IdPlatillo).descripcion,
                    precio = plato.PrecioActual,
                    stock = plato.Stock,
                    pedido = plato.Solicitados
                };

                MenuOptions.Add(response);
            }

            return MenuOptions;
        }

        public MenuOptionResponse ModificarMenuOption(Guid idMenuOption, MenuOptionRequest MenuOption)
        {
            var menuPlato = new MenuOption
            {
                Stock = MenuOption.stock,
                Solicitados = MenuOption.solicitados
            };

            var found = _command.UpdateMenuOption(idMenuOption, menuPlato);
            return GetMenuOptionById(idMenuOption);
        }
    }
}
