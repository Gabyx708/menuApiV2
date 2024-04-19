using FluentValidation;

namespace Application.UseCase.V2.Menu.GetById
{
    internal class GetMenuByIdValidation : AbstractValidator<string>
    {
        public GetMenuByIdValidation()
        {
            RuleFor(s => s).NotEmpty();
            RuleFor(s => s)
                   .Must(BeValidGuid)
                   .WithMessage("ID must be of GUID type");
        }

        private bool BeValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
