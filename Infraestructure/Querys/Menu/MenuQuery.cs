using Application.Interfaces.IMenu;
using Domain.Dtos;
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
            return _context.Menues.Find(idMenu)  ?? throw new NullReferenceException();
        }

        public Menu GetNextMenu()
        {
            //TODO: codificar para traer el menu siguiente a pedir
            throw new NotImplementedException();
        }

        public PaginatedList<Menu> GetMenuList(DateTime InitialDate,DateTime FinalDate,int index,int quantity) 
        { 
        
        }
    }
}
