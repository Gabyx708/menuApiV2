using Application.Helpers.Encrypt;
using Application.Interfaces.IAuthentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.UseCase.V2.User.SignIn
{
    public class UserSignInCommand : ISignIn
    {
        private readonly IAuthenticationQuery authenticacionQuery;
        private readonly string secret;

        public UserSignInCommand(IAuthenticationQuery authenticacionQuery, string secret)
        {
            this.authenticacionQuery = authenticacionQuery;
            this.secret = secret;
        }

        public Result<SignInResponse> SignIn(SignInRequest request)
        {

            string Encryptedpassword = Encrypt256.GetSHA256(request.Password);
            Domain.Entities.User authenticateUser;

            try
            {
                authenticateUser = authenticacionQuery.AuthenticateUser(request.UserName, Encryptedpassword);
            }
            catch(NullReferenceException)
            {
                return Result<SignInResponse>.UnauthrorizedResult("The password or username is incorrect");
            }

            string token = this.generateTokenAutentication(authenticateUser);

            var response = new SignInResponse
            {
                Id = authenticateUser.IdUser,
                Name = authenticateUser.Name,
                LastName = authenticateUser.LastName,
                NickName = authenticateUser.NickName,
                Token = token
            };

            return Result<SignInResponse>.SuccessResult(response);
        }

        private string generateTokenAutentication(Domain.Entities.User user)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddHours(1); // Define la hora de expiración

            var header = new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    SecurityAlgorithms.HmacSha256
                ));

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.IdUser),
                new Claim(JwtRegisteredClaimNames.Name, $"{user.Name} {user.LastName}")
            };

            var payload = new JwtPayload
            {
                {"sub", user.IdUser},
                {"name", $"{user.Name} {user.LastName}"},
                {"id",$"{user.IdUser}"},
                {"exp", new DateTimeOffset(expires).ToUnixTimeSeconds()}, // Agrega la fecha de expiración
                {"rol",user.Privilege },
                {"aud","app-frontend"},
                {"iss","menu-service"}
            };

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
