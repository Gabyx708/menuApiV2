using Application.Interfaces.IMenu;
using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
            return _context.Menues.
                           Include(menu => menu.Options)
                           .ThenInclude(menuOption => menuOption.Dish)
                           .SingleOrDefault(menu => menu.IdMenu == idMenu)
                            ?? throw new NullReferenceException();
        }

        public Menu GetNextAvailableMenu()
        {
            var currentDate = DateTime.Now.Date;

            return _context.Menues
                            .Where(m => m.EatingDate >= currentDate)
                            .Where(m => m.CloseDate >= currentDate)
                            .OrderBy(m => m.CloseDate)
                            .Include(menu => menu.Options)
                            .ThenInclude(menuOption => menuOption.Dish)
                            .FirstOrDefault()
                            ?? throw new NullReferenceException();
        }

        public PaginatedList<Menu> GetMenuList(DateTime? InitialDate, DateTime? FinalDate, int index, int quantity)
        {
            var menues = from m in _context.Menues select m;

            if (InitialDate != null)
            {
                menues = menues.Where(m => m.UploadDate.Date > InitialDate);
            }

            if (FinalDate != null)
            {
                menues = menues.Where(m => m.CloseDate.Date < FinalDate);
            }

            menues = menues.OrderByDescending(m => m.EatingDate);

            return PaginatedList<Menu>.Create(menues, index, quantity);
        }

        public PaginatedList<Menu> GetAll(int index, int quantity)
        {
            var menues = from m in _context.Menues select m;

            var page = PaginatedList<Menu>.Create(menues, index, quantity);

            return page;
        }

    }
}
