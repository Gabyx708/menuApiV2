using Application.Common.Models;
using Application.Interfaces.IUser;
using Application.UseCase.V2.Order.Create;
using Application.UseCase.V2.User.Create;
using Application.UseCase.V2.User.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserCommand _createUserCommand;
        private readonly IGetUsers _getAllUsers;

        public UserController(ICreateUserCommand createUserCommand, IGetUsers getAllUsers)
        {
            _createUserCommand = createUserCommand;
            _getAllUsers = getAllUsers;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserResponse), 201)]
        [ProducesResponseType(typeof(SystemResponse), 409)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        public IActionResult CreateNewUser(CreateUserRequest request)
        {
            var result = _createUserCommand.CreateNewMenuUser(request);

            if (result.Success)
            {
                return Created("", result.Data);
            }

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };

        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedListResponse<UserResponse>),200)]
        public IActionResult GetAllUsersList(int? index,int? quantity)
        {
            var result = _getAllUsers.GetAllUsers(index ?? 1,quantity ?? 10);

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
