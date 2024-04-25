using Application.Interfaces.IMenuOption;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class MenuOptionCommand : IMenuOptionCommand
    {
        private readonly MenuAppContext _context;

        public MenuOptionCommand(MenuAppContext context)
        {
            _context = context;
        }

        public MenuOption UpdateMenuOption(MenuOption MenuOption)
        {
            var existMenuOption = _context.MenuOptions.Find(MenuOption.IdMenu,MenuOption.IdDish);

            if (existMenuOption == null)
            {
                 throw new NullReferenceException();
            }

            existMenuOption.Price = MenuOption.Price;
            existMenuOption.Stock = MenuOption.Stock;
            existMenuOption.Requested = MenuOption.Requested;

            if (existMenuOption.Stock < existMenuOption.Requested) 
            { 
                throw new InvalidOperationException();
            }

            _context.Update(existMenuOption);
            return existMenuOption;
        }
    }
}
