using Application.Interfaces.IMenu;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class MenuQuery : IMenuQuery
    {
        private readonly MenuAppContext _context;

        public MenuQuery(MenuAppContext context)
        {
            _context = context;
        }

        public Menu GetMenuById(Guid idMenu)
        {
            var menuEncontrado = _context.Menues.FirstOrDefault(m => m.IdMenu == idMenu);
            return menuEncontrado;
        }

        public List<MenuPlatillo> PlatillosDelMenu(Guid idMenu)
        {
            return _context.MenuPlatillos.Where(mp => mp.IdMenu == idMenu).ToList();
        }

        public Menu GetByDateConsumo(DateTime fechaConsumo)
        {
            var found = _context.Menues.FirstOrDefault(m => m.FechaConsumo.Date == fechaConsumo);

            if (found != null)
            {
                return found;
            }

            return null;
        }

        public Menu GetUltimoMenu()
        {
            var found = _context.Menues.OrderByDescending(m => m.FechaConsumo).FirstOrDefault();
            return found;
        }
    }
}
