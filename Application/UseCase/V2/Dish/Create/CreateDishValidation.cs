using FluentValidation;

namespace Application.UseCase.V2.Dish.Create
{
    internal class CreateDishValidation : AbstractValidator<CreateDishRequest>
    {
        public CreateDishValidation()
        {
            RuleFor(d => d.Price)
                    .GreaterThan(100)
                    .WithMessage("The price must be greater than 100");

            RuleFor(d => d.Description)
                    .NotEmpty()
                    .WithMessage("Description cannot be empty")
                    .MinimumLength(10)
                    .WithMessage("Description must be at leates 10 characterers long");
        }
    }
}
