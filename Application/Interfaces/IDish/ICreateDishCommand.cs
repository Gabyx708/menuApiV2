using Application.UseCase.V2.Dish.Create;
using Domain.Entities;

namespace Application.Interfaces.IDish
{
    public interface ICreateDishCommand
    {
        Result<CreateDishResponse> CreateDish(CreateDishRequest createDishRequest);
    }
}
