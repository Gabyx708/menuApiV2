using Application.Common.Models;
using Application.Interfaces.IAuthentication;
using Application.UseCase.V2.User.SignIn;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ISignIn _signInService;

        public LoginController(ISignIn signInService)
        {
            _signInService = signInService;
        }

        [HttpPost]
        public IActionResult LoginUser(SignInRequest request)
        {
            var result = _signInService.SignIn(request);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };
        }
    }
}
