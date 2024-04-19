using Domain.Dtos;
using Domain.Entities;

namespace Application.Interfaces.IDish
{
    public interface IDishQuery
    {
        Dish GetDishById(int idDish);
        PaginatedList<Dish> GetAll(int index,int quantity);
        PaginatedList<Dish> GetByDescription(int index,string description);

    }
}
