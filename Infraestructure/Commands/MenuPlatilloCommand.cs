using Application.Interfaces.IMenuPlatillo;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class MenuPlatilloCommand : IMenuPlatilloCommand
    {
        private readonly MenuAppContext _context;

        public MenuPlatilloCommand(MenuAppContext context)
        {
            _context = context;
        }

        public MenuPlatillo AsignarPlatilloAMenu(Guid idMenu, int idPlatillo, int stock)
        {
            var platillo = _context.Platillos.Single(p => p.IdPlatillo == idPlatillo);

            var NuevoMenuPlatillo = new MenuPlatillo
            {
                IdMenu = idMenu,
                IdPlatillo = platillo.IdPlatillo,
                PrecioActual = platillo.Precio,
                Stock = stock,
                Solicitados = 0
            };

            return CreateMenuPlatillo(NuevoMenuPlatillo);
        }

        public MenuPlatillo CreateMenuPlatillo(MenuPlatillo menuPlatillo)
        {
            _context.Add(menuPlatillo);
            _context.SaveChanges();
            return menuPlatillo;
        }

        public MenuPlatillo UpdateMenuPlatillo(Guid idMenuPlatillo, MenuPlatillo menuPlatillo)
        {
            var found = _context.MenuPlatillos.FirstOrDefault(mp => mp.IdMenuPlatillo == idMenuPlatillo);

            if (found != null)
            {
                found.IdMenuPlatillo = found.IdMenuPlatillo;
                found.IdPlatillo = found.IdPlatillo;
                found.PrecioActual = found.PrecioActual;
                found.Stock = menuPlatillo.Stock;
                found.Solicitados = menuPlatillo.Solicitados;
                _context.Update(found);
                _context.SaveChanges();
            };

            return null;
        }
    }
}
