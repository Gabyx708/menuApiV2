using Application.Interfaces.IMenu;
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
        private readonly IMenuOptionService _serviceMenuOption;

        public MenuService(IMenuCommand command, IMenuQuery query, IMenuOptionService serviceMenuOption)
        {
            _command = command;
            _query = query;
            _serviceMenuOption = serviceMenuOption;
        }

        public MenuResponse CreateMenu(MenuRequest request)
        {
            var nuevoMenu = new Menu
            {
                EatingDate = request.fecha_consumo,
                CloseDate = request.fecha_cierre,
                UploadDate = DateTime.Now
            };

            if (nuevoMenu.EatingDate < nuevoMenu.UploadDate || nuevoMenu.EatingDate < nuevoMenu.CloseDate)
            {
                throw new InvalidOperationException();
            }

            _command.InsertMenu(nuevoMenu);
            Logger.LogInformation("create new menu: {@menu}", nuevoMenu);

            _serviceMenuOption.AsignarPlatillosAMenu(nuevoMenu.IdMenu, request.platillosDelMenu);

            return new MenuResponse
            {
                id = nuevoMenu.IdMenu,
                fecha_consumo = nuevoMenu.EatingDate,
                fecha_carga = nuevoMenu.UploadDate,
                fecha_cierre = nuevoMenu.CloseDate,
                platillos = _serviceMenuOption.GetMenuOptionDelMenu(nuevoMenu.IdMenu)
            };
        }

        public MenuResponse GetMenuById(Guid id)
        {
            var menuRecuperado = _query.GetMenuById(id);

            return new MenuResponse
            {
                id = menuRecuperado.IdMenu,
                fecha_consumo = menuRecuperado.EatingDate,
                fecha_carga = menuRecuperado.UploadDate,
                fecha_cierre = menuRecuperado.CloseDate,
                platillos = _serviceMenuOption.GetMenuOptionDelMenu(menuRecuperado.IdMenu)
            };
        }

        public MenuResponse GetUltimoMenu()
        {
            var menuEatingDate = _query.GetUltimoMenu();

            if (menuEatingDate != null)
            {
                return GetMenuById(menuEatingDate.IdMenu);
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
