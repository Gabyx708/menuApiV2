using Domain.Entities;

namespace Application.Interfaces.IAuthentication
{
    public interface IAuthenticacionQuery
    {
        User Autenticarse(string username, string password);
    }
}
