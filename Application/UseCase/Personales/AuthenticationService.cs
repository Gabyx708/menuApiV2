using Application.Interfaces.IAuthentication;
using Application.Interfaces.IPersonal;
using Application.Request.PersonalRequests;
using Application.Request.UsuarioLoginRequests;
using Application.Response.PersonalResponses;
using Application.Response.UsuarioLoginResponse;
using Application.Tools.Encrypt;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.UseCase.Personales
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticacionQuery _query;
        private readonly IPersonalService _personalService;
        private readonly IPersonalCommand _personalCommand;
        private readonly IPersonalQuery _personalQuery;
        private readonly string _secret;
        public AuthenticationService(string secret, IAuthenticacionQuery query, IPersonalService personalService, IPersonalQuery personalQuery, IPersonalCommand personalCommand)
        {
            _secret = secret;
            _query = query;
            _personalService = personalService;
            _personalQuery = personalQuery;
            _personalCommand = personalCommand;
        }

        public UsuarioLoginResponse autenticarUsuario(UsuarioLoginRequest request)
        {
            var persona = _query.Autenticarse(request.username, request.password);
            if (persona == null) { return null; };

            var token = generateTokenAutentication(persona);

            return new UsuarioLoginResponse
            {
                id = persona.IdPersonal,
                Nombre = persona.Nombre,
                Apellido = persona.Apellido,
                Token = token
            };
        }

        public PersonalResponse changeUserPassword(Guid idUser, PersonalPasswordRequest request)
        {
            string requestOriginalPassword = Encrypt256.GetSHA256(request.originalPassword);
            string nuevaPassword = Encrypt256.GetSHA256(request.newPassword);

            var personal = _personalQuery.GetPersonalById(idUser);
            string originalPassword = personal.Password;

            if (personal != null && originalPassword == requestOriginalPassword)
            {
                personal.Password = nuevaPassword;
                _personalCommand.updatePersonal(idUser, personal);
                return _personalService.GetPersonalById(idUser);
            }

            return null;
        }

        public PersonalResponse resetPassword(Guid idUser)
        {
            var personal = _personalQuery.GetPersonalById(idUser);

            if (personal != null)
            {
                var passwordRest = Encrypt256.GetSHA256(personal.Dni);
                personal.Password = passwordRest;
                _personalCommand.updatePersonal(idUser, personal);
                return _personalService.GetPersonalById(idUser);
            }

            return null;
        }

        public string generateTokenAutentication(Personal usuario)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddHours(1); // Define la hora de expiración

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
                    SecurityAlgorithms.HmacSha256
                ));

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Dni),
                new Claim(JwtRegisteredClaimNames.Name, $"{usuario.Nombre} {usuario.Apellido}")
            };

            var payload = new JwtPayload
            {
                {"sub", usuario.Dni},
                {"name", $"{usuario.Nombre} {usuario.Apellido}"},
                {"id",$"{usuario.IdPersonal}"},
                {"exp", new DateTimeOffset(expires).ToUnixTimeSeconds()}, // Agrega la fecha de expiración
                {"rol",usuario.Privilegio },
                {"aud","menu"},
                {"iss","menuServ"}
            };

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
