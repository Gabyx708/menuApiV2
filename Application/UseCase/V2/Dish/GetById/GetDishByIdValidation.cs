using FluentValidation;

namespace Application.UseCase.V2.Dish.GetById
{
    internal class GetDishByIdValidation : AbstractValidator<string>
    {
        public GetDishByIdValidation()
        {
            RuleFor(s => s).NotEmpty()
                            .Must(BeValidId)
                            .WithMessage("The ID is invalid, it must be a number greater than zero");
        }

        private bool BeValidId(string id)
        {
            int result;
            bool isNumber = int.TryParse(id,out result);

            if (isNumber && result > 0)
            {
                return true;
            }

            return false;
        }

    }
}
