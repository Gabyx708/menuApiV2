using Application.Request.PersonalRequests;
using Application.Request.UsuarioLoginRequests;
using Application.Response.PersonalResponses;
using Application.Response.UsuarioLoginResponse;
using Domain.Entities;

namespace Application.Interfaces.IAuthentication
{
    public interface IAuthenticationService
    {
        UsuarioLoginResponse autenticarUsuario(UsuarioLoginRequest usuario);
        PersonalResponse changeUserPassword(Guid idUser, PersonalPasswordRequest request);
        PersonalResponse resetPassword(Guid idUser);
        string generateTokenAutentication(Personal usuario);
    }
}
