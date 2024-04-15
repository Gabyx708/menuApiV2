using Application.Interfaces.IPlatillo;
using Application.Request.PlatilloRequests;
using Application.Response.PlatilloResponses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1.3/[controller]")]
    [ApiController]
    public class PlatilloController : ControllerBase
    {
        private readonly IPlatilloService _services;

        public PlatilloController(IPlatilloService services)
        {
            _services = services;
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
        public IActionResult CrearPlatillo(PlatilloRequest request)
        {
            var nuevoPlato = _services.CreatePlatillo(request);
            return new JsonResult(nuevoPlato) { StatusCode = 201 };
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
