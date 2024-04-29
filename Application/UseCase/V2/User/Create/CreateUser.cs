using Application.Helpers.Encrypt;
using Application.Interfaces.IUser;

namespace Application.UseCase.V2.User.Create
{
    public class CreateUser : ICreateUserCommand
    {
        private readonly IUserCommand userCommand;
        private readonly IUserQuery userQuery;

        public CreateUser(IUserCommand userCommand, IUserQuery userQuery)
        {
            this.userCommand = userCommand;
            this.userQuery = userQuery;
        }

        public Result<CreateUserResponse> CreateNewMenuUser(CreateUserRequest request)
        {
            var validator = new CreateUserValidation(userQuery);
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                var conflictError = validationResult.Errors.FirstOrDefault(e => e.ErrorCode == "409");

                if (conflictError != null)
                {
                    return Result<CreateUserResponse>.ConflictResult(errorMessages);
                }

                return Result<CreateUserResponse>.ValidationResult(errorMessages);
            }

            var user = new Domain.Entities.User
            {
                IdUser = request.IdUser.Trim(),
                Name = request.Name,
                LastName = request.LastName,
                NickName = request.Name,
                BirthDate = request.BirthDate,
                RegistrationDate = DateTime.Now,
                Privilege = request.Privilege,
                Password = Encrypt256.GetSHA256(request.IdUser)
            };

            var newUser = userCommand.InsertUser(user);

            var userResponse = new CreateUserResponse
            {
                IdUser = newUser.IdUser,
                Name = newUser.Name,
                LastName = newUser.LastName,
                BirthDate = newUser.BirthDate,
                RegistrationDate = newUser.RegistrationDate,
                Privilege = newUser.Privilege
            };

            return Result<CreateUserResponse>.SuccessResult(userResponse);
        }
    }
}
