using FluentValidation;

namespace Application.UseCase.V2.Menu.Create
{
    internal class CreateMenuValidation : AbstractValidator<CreateMenuRequest>
    {
        public CreateMenuValidation()
        {
            RuleFor(cr => cr.EatingDate)
                    .GreaterThan(cr => cr.CloseDate)
                    .WithMessage("Close date must be after the eating date.");

            RuleFor(cr => cr.CloseDate)
                    .NotEqual(cr => cr.EatingDate)
                    .WithMessage("The two dates cannot be the same");

            RuleFor(cr => cr.Options)
                    .Must(options => options != null && options.Any())
                    .WithMessage("Options list must contain at least one element.");
        }
    }
}
