using Domain.Entities;

namespace Application.Interfaces.IAuthentication
{
    public interface IAuthenticationQuery
    {
        User AuthenticateUser(string username, string password);
    }
}
