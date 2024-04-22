using Application.Interfaces.IDish;
using Domain.Dtos;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class DishQuery : IDishQuery
    {
        private readonly MenuAppContext _context;

        public DishQuery(MenuAppContext context)
        {
            _context = context;
        }

        public PaginatedList<Dish> GetAll(int index, int quantity)
        {
            var dishes = from d in _context.Dishes select d;

            return PaginatedList<Dish>.Create(dishes, index, quantity);
        }

        public PaginatedList<Dish> GetByDescription(int index, string description,int quantity)
        {
            var dishes = _context.Dishes
                         .Where(d => d.Description.Contains(description))
                         .OrderBy(d => d.IdDish);

            return PaginatedList<Dish>.Create(dishes, index, quantity);
        }

        public Dish GetDishById(int idDish)
        {
            return _context.Dishes.Find(idDish)
                  ?? throw new NullReferenceException();
        }
    }
}
