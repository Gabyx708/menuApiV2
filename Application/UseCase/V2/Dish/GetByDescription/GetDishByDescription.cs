using Application.Common.Models;
using Application.Interfaces.IDish;

namespace Application.UseCase.V2.Dish.GetByDescription
{
    public class GetDishByDescription : IGetDishesByDescription
    {
        private readonly IDishQuery dishQuery;

        public GetDishByDescription(IDishQuery dishQuery)
        {
            this.dishQuery = dishQuery;
        }

        public Result<PaginatedListResponse<GetDishResponse>> GetDishesByDescription(string description, int index, int quantity)
        {
            var dishesPage = dishQuery.GetByDescription(index, description, quantity);

            var dishesResponse = dishesPage.Items
                                  .Select(d => ConvertDishToDishResponse(d))
                                  .ToList();

            var page = new PaginatedListResponse<GetDishResponse>
            {
                Index = dishesPage.PageIndex,
                TotalPages = dishesPage.TotalPages,
                TotalRecords = dishesPage.TotalRecords,
                Items = dishesResponse,
            };

            return Result<PaginatedListResponse<GetDishResponse>>.SuccessResult(page);
        }

        private static GetDishResponse ConvertDishToDishResponse(Domain.Entities.Dish dish)
        {
            return new GetDishResponse
            {
                Id = dish.IdDish,
                Description = dish.Description,
                Price = dish.Price
            };
        }
    }
}
