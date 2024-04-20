
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
                return Result<GetMenuByIdResponse>.ValidationResult(errorMessages);
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

                return Result<GetMenuByIdResponse>.NotFoundResult($"Menu with id {idMenu} not found");

            }
            catch (NullReferenceException)
            { 
                    return Result<GetMenuByIdResponse>.NotFoundResult($"Menu with id {idMenu} not found");
            }
            catch (Exception ex)
            {
                return Result<GetMenuByIdResponse>.FailureResult($"An error occurred while fetching the menu: {ex.Message}");
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
                    Requested = option.Requested
                };

                optionsResponse.Add(response);
            }

            return optionsResponse;
        }


    }
}
