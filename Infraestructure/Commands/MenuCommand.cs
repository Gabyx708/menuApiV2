using Application.Interfaces.IMenu;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class MenuCommand : IMenuCommand
    {
        private readonly MenuAppContext _context;

        public MenuCommand(MenuAppContext context)
        {
            _context = context;
        }

        public Menu InsertMenu(Menu menu)
        {
            _context.Add(menu);
            _context.SaveChanges();
            return menu;
        }

        public Menu DeleteMenu(Menu menu)
        {
            _context.Remove(menu);
            _context.SaveChanges();
            return menu;
        }
    }
}
