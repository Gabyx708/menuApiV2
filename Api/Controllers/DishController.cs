using Application.Common.Models;
using Application.Interfaces.IDish;
using Application.UseCase.V2.Dish.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ICreateDishCommand _createDishCommand;

        public DishController(ICreateDishCommand createDishCommand)
        {
            _createDishCommand = createDishCommand;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateDishResponse), 201)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        [ProducesResponseType(typeof(SystemResponse), 500)]
        public IActionResult CreateNewDish(CreateDishRequest request)
        {
            var result = _createDishCommand.CreateDish(request);

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
    }
}
