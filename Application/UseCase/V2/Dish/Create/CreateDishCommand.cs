using Application.Interfaces.IDish;
using Application.UseCase.V2.Menu.GetById;

namespace Application.UseCase.V2.Dish.Create
{
    public class CreateDishCommand : ICreateDishCommand
    {
        private readonly IDishCommand dishCommand;

        public CreateDishCommand(IDishCommand dishCommand)
        {
            this.dishCommand = dishCommand;
        }

        public Result<CreateDishResponse> CreateDish(CreateDishRequest request)
        {
            var validator = new CreateDishValidation();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<CreateDishResponse>.ValidationResult(errorMessages);
            }


            var dish = new Domain.Entities.Dish
            {
                Price = request.Price,
                Description = request.Description,
                Activated = true
            };

            var newDish = dishCommand.InsertDish(dish);

            var dishResponse = new CreateDishResponse
            {
                Id = newDish.IdDish,
                Description = newDish.Description,
                Price = newDish.Price,
            };

            return Result<CreateDishResponse>.SuccessResult(dishResponse);
        }
    }
}
