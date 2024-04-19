using Application.Interfaces.IMenu;

namespace Application.UseCase.V2.Menu.GetById
{
    public class GetMenuById : IGetMenuById
    {
        private readonly IMenuQuery menuQuery;

        public GetMenuById(IMenuQuery menuQuery)
        {
            this.menuQuery = menuQuery;
        }

        public Result<GetMenuByIdResponse> GetMenuByIdResponse(Guid idMenu)
        {
            try
            {
                var menuFound = menuQuery.GetMenuById(idMenu);

                if (menuFound != null)
                {
                    var menuMap = new GetMenuByIdResponse
                    {
                        Id = menuFound.IdMenu,
                        EatingDate = menuFound.EatingDate,
                        UploadDate = menuFound.UploadDate,
                        CloseDate = menuFound.CloseDate
                    };

                    return Result<GetMenuByIdResponse>.SuccessResult(menuMap);
                }
                else
                {
                    return Result<GetMenuByIdResponse>.FailureResult($"Menu with id {idMenu} not found");
                }
            }
            catch (Exception ex)
            {
                return Result<GetMenuByIdResponse>.FailureResult($"An error occurred while fetching the menu: {ex.Message}");
            }
        }


    }
}
