using Application.Common.Models;
using Application.UseCase.V2.Dish.GetByDescription;

namespace Application.Interfaces.IDish
{
    public interface IGetDishesByDescription
    {
        Result<PaginatedListResponse<GetDishResponse>> GetDishesByDescription(string description, int index, int quantity);
    }
}
