
using Application.Interfaces.IMenu;

namespace Application.UseCase.V2.Menu.GetById
{
    public class GetMenuById : IGetMenuByIdQuery
    {
        private readonly IMenuQuery menuQuery;

        public GetMenuById(IMenuQuery menuQuery)
        {
            this.menuQuery = menuQuery;
        }

        public Result<GetMenuByIdResponse> GetMenuByIdResponse(string idMenu)
        {
            var validator = new GetMenuByIdValidation();
            var validationResult = validator.Validate(idMenu);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<GetMenuByIdResponse>.FailureResult(errorMessages,400);
            }

            try
            {
                var id = Guid.Parse(idMenu);
                var menuFound = menuQuery.GetMenuById(id);

                if (menuFound != null)
                {
                    var menuResponse = new GetMenuByIdResponse
                    {
                        Id = menuFound.IdMenu,
                        EatingDate = menuFound.EatingDate,
                        UploadDate = menuFound.UploadDate,
                        CloseDate = menuFound.CloseDate,
                        Options = this.GetOptionsForMenu(menuFound)
                    };

                    return Result<GetMenuByIdResponse>.SuccessResult(menuResponse);
                }
                else
                {
                    return Result<GetMenuByIdResponse>.FailureResult($"Menu with id {idMenu} not found",404);
                }
            }
            catch (Exception ex)
            {
                return Result<GetMenuByIdResponse>.FailureResult($"An error occurred while fetching the menu: {ex.Message}",500);
            }
        }


        private List<OptionResponse> GetOptionsForMenu(Domain.Entities.Menu menu)
        {
            var originalOptions = menu.Options;
            var optionsResponse = new List<OptionResponse>();

            foreach (var option in originalOptions)
            {
                var response = new OptionResponse
                {
                    IdDish = option.Dish.IdDish,
                    Description = option.Dish.Description,
                    Price = option.Price,
                    Stock = option.Stock,
                };

                optionsResponse.Add(response);
            }

            return optionsResponse;
        }


    }
}
