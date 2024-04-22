using Application.Common.Models;
using Application.UseCase.V2.Menu.GetFilter;

namespace Application.Interfaces.IMenu
{
    public interface IGetMenuFiltered
    {
        Result<PaginatedListResponse<GetMenuFilterResponse>> GetFilterMenuByEatingDate(DateTime? initialDate,
                                                                                       DateTime? finalDate,
                                                                                       int recordQuantity,
                                                                                       int index);
    }
}
