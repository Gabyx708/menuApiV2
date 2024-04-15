using Application.Interfaces.IMenu;
using Application.Interfaces.IMenuPlatillo;
using Application.Request.MenuRequests;
using Application.Response.MenuResponses;
using Application.Tools.Log;
using Domain.Entities;

namespace Application.UseCase.Menues
{
    public class MenuService : IMenuService
    {
        private readonly IMenuCommand _command;
        private readonly IMenuQuery _query;
        private readonly IMenuPlatilloService _serviceMenuPlatillo;

        public MenuService(IMenuCommand command, IMenuQuery query, IMenuPlatilloService serviceMenuPlatillo)
        {
            _command = command;
            _query = query;
            _serviceMenuPlatillo = serviceMenuPlatillo;
        }

        public MenuResponse CreateMenu(MenuRequest request)
        {
            var nuevoMenu = new Menu
            {
                FechaConsumo = request.fecha_consumo,
                FechaCierre = request.fecha_cierre,
                FechaCarga = DateTime.Now
            };

            if (nuevoMenu.FechaConsumo < nuevoMenu.FechaCarga || nuevoMenu.FechaConsumo < nuevoMenu.FechaCierre)
            {
                throw new InvalidOperationException();
            }

            _command.CreateMenu(nuevoMenu);
            Logger.LogInformation("create new menu: {@menu}", nuevoMenu);

            _serviceMenuPlatillo.AsignarPlatillosAMenu(nuevoMenu.IdMenu, request.platillosDelMenu);

            return new MenuResponse
            {
                id = nuevoMenu.IdMenu,
                fecha_consumo = nuevoMenu.FechaConsumo,
                fecha_carga = nuevoMenu.FechaCarga,
                fecha_cierre = nuevoMenu.FechaCierre,
                platillos = _serviceMenuPlatillo.GetMenuPlatilloDelMenu(nuevoMenu.IdMenu)
            };
        }

        public MenuResponse GetMenuById(Guid id)
        {
            var menuRecuperado = _query.GetMenuById(id);

            return new MenuResponse
            {
                id = menuRecuperado.IdMenu,
                fecha_consumo = menuRecuperado.FechaConsumo,
                fecha_carga = menuRecuperado.FechaCarga,
                fecha_cierre = menuRecuperado.FechaCierre,
                platillos = _serviceMenuPlatillo.GetMenuPlatilloDelMenu(menuRecuperado.IdMenu)
            };
        }

        public MenuResponse GetUltimoMenu()
        {
            var menuFechaConsumo = _query.GetUltimoMenu();

            if (menuFechaConsumo != null)
            {
                return GetMenuById(menuFechaConsumo.IdMenu);
            }

            return null;
        }

        public MenuResponse BorrarMenu(Guid idMenu)
        {
            var found = _query.GetMenuById(idMenu);
            var foundResponse = GetMenuById(idMenu);

            if (found != null)
            {
                _command.DeleteMenu(found);
                Logger.LogInformation("delete menu: {@menu}", found.IdMenu);
                return foundResponse;
            }

            return foundResponse;
        }
    }
}
