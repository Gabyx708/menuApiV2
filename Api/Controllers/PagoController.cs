using Application.Interfaces.IPagos;
using Application.Request.PagoRequests;
using Application.Response.PagoResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1.3/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _services;

        public PagoController(IPagoService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(PagoResponse), 201)]
        public IActionResult RegistrarUnPago(PagoRequest request)
        {
            var result = _services.HacerUnPago(request);

            return new JsonResult(result) { StatusCode = 201 };
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PagoResponse), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult ConsultarPago(long id)
        {
            var result = _services.GetPagoResponseById(id);

            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result);
        }

        [Authorize]
        [HttpPatch("{id}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        public IActionResult AnularPago(long id, PagoAnularRequest request)
        {
            var result = _services.ModificarAnulacion(id, request.IsAnulado);

            if (result == null) { return NotFound(); }

            return NoContent();
        }
    }
}
