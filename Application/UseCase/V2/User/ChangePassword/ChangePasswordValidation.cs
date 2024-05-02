using FluentValidation;

namespace Application.UseCase.V2.User.ChangePassword
{
    internal class ChangePasswordValidation : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordValidation()
        {
            RuleFor(cp => cp.NewPassword)
                    .NotEmpty().WithMessage("Password is required")
                    .NotNull()
                    .MinimumLength(10).WithMessage("The password must be at least 10 characters long")
                    .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Password must be alphanumeric")
                    .Must(password => !password.Contains("The password cannot contain blank spaces"));
        }
    }
}
