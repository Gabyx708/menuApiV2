using Application.Interfaces.I;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class MenuOptionQuery : IMenuOptionQuery
    {
        private readonly MenuAppContext _context;

        public MenuOptionQuery(MenuAppContext context)
        {
            _context = context;
        }

        public MenuOption GetById(Guid idMenu, int idDish)
        {
            return _context.MenuOptions.Find(idMenu, idDish)
                    ?? throw new NullReferenceException();
        }

        public List<MenuOption> GetMenuOptionByMenuId(Guid idMenu)
        {
            return _context.MenuOptions.Where(mop => mop.IdMenu == idMenu).ToList();
        }
    }
}
