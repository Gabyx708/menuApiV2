using Application.Interfaces.IPagos;
using Application.Response.PagoResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1.3/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _services;

        public PagosController(IPagoService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<PagoResponse>), 200)]
        public IActionResult AllPagos(DateTime? fechaDesde, DateTime? fechaHasta)
        {
            List<PagoResponse> result;

            if (fechaDesde != null && fechaHasta != null)
            {

                result = _services.ObtenerPagosFiltrados((DateTime)fechaDesde, (DateTime)fechaHasta);
            }
            else
            {
                result = _services.GetAll();
            }


            if (result == null)
            {
                return NotFound();
            }

            return new JsonResult(result) { StatusCode = 200 };
        }
    }
}
