using Application.Common.Models;
using Application.UseCase.V2.User.ChangePassword;

namespace Application.Interfaces.ISession
{
    public interface IChangePassword
    {
        Result<SystemResponse> ChangePassword(ChangePasswordRequest request);
        Result<SystemResponse> ResetPassword(string idUser,string idUserAdministrator);
    }
}
