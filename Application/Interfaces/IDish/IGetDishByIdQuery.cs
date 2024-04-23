using Application.UseCase.V2.Dish.GetById;

namespace Application.Interfaces.IDish
{
    public interface IGetDishByIdQuery
    {
        Result<GetDishByIdResponse> GetDishById(string id);
    }
}
