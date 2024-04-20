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

        public Menu InsertMenu(Menu menu,List<MenuOption> options)
        {
            _context.Add(menu);

            foreach (var option in options)
            {

                var dish = _context.Dishes.Find(option.IdDish);

                if (dish == null) { throw new NullReferenceException(); };

                    option.IdMenu = menu.IdMenu;
                    option.Price = dish.Price;

                    _context.MenuOptions.Add(option);

                    menu.Options.Add(option);
                          
            }

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
