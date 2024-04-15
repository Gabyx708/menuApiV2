using Application.Interfaces.IDescuento;
using Application.Request.DescuestoRequests;
using Application.Response.DescuentoResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1.3/[controller]")]
    [ApiController]
    public class DescuentoController : ControllerBase
    {
        private readonly IDescuentoService _services;

        public DescuentoController(IDescuentoService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DescuentoResponse), 200)]
        public IActionResult GetDescuento(Guid id)
        {
            var descuento = _services.GetDescuentoById(id);
            return new JsonResult(descuento) { StatusCode = 200 };
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(DescuentoResponse), 200)]
        public IActionResult GetVigente()
        {
            var vigente = _services.GetDescuentoVigente();
            return new JsonResult(vigente) { StatusCode = 200 };
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(DescuentoResponse), 201)]
        public IActionResult CreateDescuento(DescuentoRequest request)
        {
            var nuevoDescuento = _services.crearDescuento(request);
            return new JsonResult(nuevoDescuento) { StatusCode = 201 };
        }
    }
}
