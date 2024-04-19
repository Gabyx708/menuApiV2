using Domain.Entities;

namespace Application.Interfaces.IDish
{
    public interface IDishCommand
    {
        public Dish InsertDish(Dish dish);
        public Dish UpdateDish(Dish dish);
    }
}
