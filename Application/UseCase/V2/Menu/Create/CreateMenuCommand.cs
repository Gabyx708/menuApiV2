using Application.Interfaces.IMenu;
using Application.UseCase.V2.Menu.GetById;
using Domain.Entities;

namespace Application.UseCase.V2.Menu.Create
{
    public class CreateMenuCommand : ICreateMenuCommand
    {
        private readonly IMenuCommand menuCommand;

        public CreateMenuCommand(IMenuCommand menuCommand)
        {
            this.menuCommand = menuCommand;
        }

        public Result<CreateMenuResponse> CreateMenu(CreateMenuRequest request)
        {
            var validator = new CreateMenuValidation();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<CreateMenuResponse>.ValidationResult(errorMessages);
            }

            var optionsRequests = request.Options;

            var menu = new Domain.Entities.Menu
            {
                CloseDate = request.CloseDate,
                UploadDate = DateTime.Now,
                EatingDate = request.EatingDate,
            };

            var menuOptions = new List<MenuOption>();

            foreach (var option in optionsRequests)
            {
                var optionMenu = new MenuOption
                {
                    IdDish = option.IdDish,
                    Stock = option.Stock
                };

                menuOptions.Add(optionMenu);
            }

            try
            {
                menuCommand.InsertMenu(menu, menuOptions);
            }
            catch (NullReferenceException)
            {
                return Result<CreateMenuResponse>.ValidationResult("It's likely that a dish id doesn't exist");
            }

            var createMenuResponse = new CreateMenuResponse
            {
                Id = menu.IdMenu,
                CloseDate = menu.CloseDate,
                EatingDate = menu.EatingDate,
                UploadDate = menu.UploadDate,
                Options = null
            };

            return Result<CreateMenuResponse>.SuccessResult(createMenuResponse);
        }

    }
}
