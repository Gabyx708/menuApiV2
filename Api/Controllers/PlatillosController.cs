using Application.Interfaces.Dish;
using Application.Response.PlatilloResponses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1.3/[controller]")]
    [ApiController]
    public class PlatillosController : ControllerBase
    {
        private readonly IPlatilloService _services;

        public PlatillosController(IPlatilloService services)
        {
            _services = services;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        public IActionResult AlterarPrecios(decimal nuevoPrecio)
        {
            var result = _services.AlterarPreciosMasivamente(nuevoPrecio);


            if (result) { return NoContent(); }

            return new JsonResult("ocurrio un problema durante los cambios");
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<PlatilloResponse>), 200)]
        public IActionResult GetAll()
        {
            var platillos = _services.GetAll();
            return new JsonResult(platillos) { StatusCode = 200 };
        }
    }
}
