using Domain.Entities;

namespace Application.Interfaces.IAuthentication
{
    public interface IAuthenticacionQuery
    {
        Personal Autenticarse(string username, string password);
    }
}
