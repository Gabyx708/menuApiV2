using Application.Common.Models;
using Application.Helpers.Encrypt;
using Application.Interfaces.IAuthentication;
using Application.Interfaces.ISession;
using Application.Interfaces.IUser;
using Domain.Enums;

namespace Application.UseCase.V2.User.ChangePassword
{
    public class ChangePasswordCommand : IChangePassword
    {
        private readonly IUserQuery userQuery;
        private readonly IUserCommand userCommand;
        private readonly IAuthenticationQuery authenticacionQuery;

        public ChangePasswordCommand(IAuthenticationQuery authenticacionQuery, IUserCommand userCommand, IUserQuery userQuery)
        {
            this.authenticacionQuery = authenticacionQuery;
            this.userCommand = userCommand;
            this.userQuery = userQuery;
        }

        public Result<SystemResponse> ChangePassword(ChangePasswordRequest request)
        {

            var validator = new ChangePasswordValidation();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return Result<SystemResponse>.ValidationResult(errorMessages);
            }

            var EncryptedOldpassword = Encrypt256.GetSHA256(request.OldPassword);
            var EncryptedNewpassword = Encrypt256.GetSHA256(request.NewPassword);
            Domain.Entities.User user = null!;

            try
            {
                user = authenticacionQuery.AuthenticateUser(request.username, EncryptedOldpassword);
            }
            catch (NullReferenceException)
            {
                return Result<SystemResponse>.UnauthrorizedResult("The password or username is incorrect");
            }

            user.Password = EncryptedNewpassword;
            userCommand.UpdateUser(user);

            var response = new SystemResponse
            {
                Message = "Password was successfully updated",
                StatusCode = 200
            };

            return Result<SystemResponse>.SuccessResult(response);
        }

        public Result<SystemResponse> ResetPassword(string idUser, string idUserAdministrator)
        {
            var adminUser = userQuery.GetUserById(idUserAdministrator);

            if(adminUser.Privilege == (int)PrivilgeCode.administrator)
            {
                var userToChange = userQuery.GetUserById(idUser);
                userToChange.Password = Encrypt256.GetSHA256(userToChange.IdUser);
                userCommand.UpdateUser(userToChange);

                var response = new SystemResponse
                {
                    Message = $"User {idUser} password was reset",
                    StatusCode = 200
                };

                return Result<SystemResponse>.SuccessResult(response);
            }

                return Result<SystemResponse>.UnauthrorizedResult("Only an administrator can reset passwords");
        }
    }
}
