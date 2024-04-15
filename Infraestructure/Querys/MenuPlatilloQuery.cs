using Application.Interfaces.IMenuPlatillo;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class MenuPlatilloQuery : IMenuPlatilloQuery
    {
        private readonly MenuAppContext _context;

        public MenuPlatilloQuery(MenuAppContext context)
        {
            _context = context;
        }

        public MenuPlatillo GetById(Guid idMenuPlatillo)
        {
            var menuPlatilloRecuperado = _context.MenuPlatillos.Single(mp => mp.IdMenuPlatillo == idMenuPlatillo);
            menuPlatilloRecuperado.Menu = _context.Menues.FirstOrDefault(m => m.IdMenu == menuPlatilloRecuperado.IdMenu);
            return menuPlatilloRecuperado;
        }

        public List<MenuPlatillo> GetMenuPlatilloByMenuId(Guid idMenu)
        {
            return _context.MenuPlatillos.Where(mp => mp.IdMenu == idMenu).ToList();
        }
    }
}
