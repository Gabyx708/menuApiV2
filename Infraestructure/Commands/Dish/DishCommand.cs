using Application.Interfaces.IDish;
using Infraestructure.Persistence;

namespace Infraestructure.Commands.Dish
{
    public class DishCommand : IDishCommand
    {
        private readonly MenuAppContext _context;

        public DishCommand(MenuAppContext context)
        {
            _context = context;
        }

        public Domain.Entities.Dish InsertDish(Domain.Entities.Dish dish)
        {
            _context.Dishes.Add(dish);
            _context.SaveChanges();

            return dish;
        }

        public Domain.Entities.Dish UpdateDish(Domain.Entities.Dish dish)
        {
            var existingDish = _context.Dishes.Find(dish.IdDish);

            if (existingDish == null)
            {
                throw new InvalidOperationException($"Dish with ID {dish.IdDish} not found.");
            }

            existingDish.Description = dish.Description;
            existingDish.Price = dish.Price;
            existingDish.Activated = dish.Activated;

            _context.SaveChanges();

            return existingDish;
        }
    }
}
