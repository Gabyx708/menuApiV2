using Application.Interfaces.Dish;
using Application.Interfaces.IDish;
using Application.Request.PlatilloRequests;
using Application.Response.PlatilloResponses;
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlatilloResponse), 200)]
        public IActionResult GetPLatillo(int id)
        {
            var platillo = _services.GetPlatilloById(id);
            return new JsonResult(platillo) { StatusCode = 200 };
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlatilloResponse), 201)]
        public IActionResult CreateNewDish(CreateDishRequest request)
        {
            var result = _createDishCommand.CreateDish(request);

            if (result.Success)
            {
                return Created(result.Data);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PlatilloResponse), 200)]
        public IActionResult CambiarPrecio(int id, PlatilloRequest request)
        {
            var platoPrecio = _services.UpdatePrecio(id, request.precio);
            return new JsonResult(platoPrecio) { StatusCode = 200 };
        }
    }
}
