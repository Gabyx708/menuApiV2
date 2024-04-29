using Application.Interfaces.IUser;
using FluentValidation;

namespace Application.UseCase.V2.User.Create
{
    internal class CreateUserValidation : AbstractValidator<CreateUserRequest>
    {
        private readonly IUserQuery userQuery;
        public CreateUserValidation(IUserQuery userQuery)
        {
            this.userQuery = userQuery;

            RuleFor(cu => cu.IdUser)
                    .NotEmpty()
                    .MinimumLength(5)
                    .Must(id => !ExistingUser(id))
                    .WithMessage("User already exists")
                    .WithErrorCode("409");

            RuleFor(cu => cu.Name)
                   .NotEmpty()
                   .MinimumLength(4);

            RuleFor(cu => cu.LastName)
                   .NotEmpty()
                   .MinimumLength(4);

            RuleFor(cu => cu.BirthDate)
                    .NotEmpty()
                    .NotNull()
                    .Must(BeAtLeastTenYearsOld)
                    .WithMessage("Birth date must be at least 10 years ago");
        }

        private bool BeAtLeastTenYearsOld(DateTime birthDate)
        {
            var tenYearsAgo = DateTime.Today.AddYears(-10);
            return birthDate <= tenYearsAgo;
        }

        private bool ExistingUser(string id)
        {
            try
            {
                var userFound = userQuery.GetUserById(id);
                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
    }
}
