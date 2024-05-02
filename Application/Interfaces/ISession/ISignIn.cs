using Application.Request.PersonalRequests;
using Application.Response.PersonalResponses;
using Application.UseCase.V2.User.SignIn;

namespace Application.Interfaces.IAuthentication
{
    public interface ISignIn
    {
        Result<SignInResponse> SignIn(SignInRequest request);
    }
}
