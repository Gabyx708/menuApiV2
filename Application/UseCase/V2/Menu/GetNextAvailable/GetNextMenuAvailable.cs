using Application.Common.Models;
using Application.Interfaces.IMenu;
using Application.UseCase.V2.Menu.GetById;

namespace Application.UseCase.V2.Menu.GetNextAvailable
{
    public class GetNextMenuAvailable : IGetNextMenuAvailable
    {
        private readonly IMenuQuery menuQuery;

        public GetNextMenuAvailable(IMenuQuery menuQuery)
        {
            this.menuQuery = menuQuery;
        }

        public Result<GetNextMenuResponse> GetNextMenuAvailableForUsers()
        {
            Domain.Entities.Menu nextMenu;

            try
            {
                nextMenu = menuQuery.GetNextAvailableMenu();
            }catch(NullReferenceException)
            {
                return Result<GetNextMenuResponse>.NotFoundResult("There is no menu available");
            }
            catch (Exception)
            {
                return Result<GetNextMenuResponse>.FailureResult("A catastrophe occurred");
            }

            var result = new GetNextMenuResponse
            {
                Id = nextMenu.IdMenu,
                EatingDate = nextMenu.EatingDate,
                CloseDate = nextMenu.CloseDate,
                UploadDate = nextMenu.UploadDate,
                Options = this.GetOptionsForMenu(nextMenu)
            };

            return Result<GetNextMenuResponse>.SuccessResult(result);
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
