using Application.Common.Models;
using Application.Interfaces.IMenu;

namespace Application.UseCase.V2.Menu.GetFilter
{
    public class GetMenuByFiltered : IGetMenuFiltered
    {
        private readonly IMenuQuery menuQuery;

        public GetMenuByFiltered(IMenuQuery menuQuery)
        {
            this.menuQuery = menuQuery;
        }

        public Result<PaginatedListResponse<GetMenuFilterResponse>> GetFilterMenuByUploadDate(DateTime? initialDate,
                                                                                              DateTime? finalDate,
                                                                                              int index)
        {

            if (initialDate == finalDate || initialDate > finalDate)
            {
                string message = $"The dates cannot be the same, the initial date cannot be older than the final date";
                return Result<PaginatedListResponse<GetMenuFilterResponse>>.ValidationResult(message);
            }

            int recordQuantity = 10;
            var menuesPage = menuQuery.GetMenuList(initialDate, finalDate, index,recordQuantity);

            var menuesResponses = menuesPage.Items
                                  .Select(m => ToGetMenuFilterResponse(m))
                                  .ToList();


            var page = new PaginatedListResponse<GetMenuFilterResponse>
            {
                Index = menuesPage.PageIndex,
                TotalPages = menuesPage.TotalPages,
                TotalRecords = menuesPage.TotalRecords,
                Items = menuesResponses
            };

            return Result<PaginatedListResponse<GetMenuFilterResponse>>.SuccessResult(page);
        }

        private static GetMenuFilterResponse ToGetMenuFilterResponse(Domain.Entities.Menu menu)
        {
            return new GetMenuFilterResponse
            {
                IdMenu = menu.IdMenu,
                CloseDate = menu.CloseDate,
                EatingDate = menu.EatingDate,
                UploadDate = menu.UploadDate
            };
        }
    }
}
