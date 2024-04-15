using Application.Interfaces.ICostos;
using Application.Response.CostoResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1.3/[controller]")]
    [ApiController]
    public class CostoController : Controller
    {

        private readonly ICostoService _services;

        public CostoController(ICostoService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(CostoDiaResponse), 200)]
        public IActionResult GetCostoDia(DateTime fecha)
        {
            var costo = _services.GetCostosDia(fecha);
            return new JsonResult(costo) { StatusCode = 200 };
        }

        [Authorize]
        [HttpGet("personal")]
        [ProducesResponseType(typeof(CostoPersonalResponse), 200)]
        public IActionResult GetCostosPersonal(DateTime fechaInicio, DateTime fechaFin, Guid idPersonal)
        {
            var costos = _services.GetCostosPersonal(fechaInicio, fechaFin, idPersonal);
            return new JsonResult(costos) { StatusCode = 200 };
        }

        [Authorize]
        [HttpGet("periodo")]
        [ProducesResponseType(typeof(CostoPeriodoResponse), 200)]
        public IActionResult GetCostosPeriodo(DateTime fechaInicio, DateTime fechaFin)
        {
            var costosPeriodo = _services.GetCostosPeriodo(fechaInicio, fechaFin);
            return new JsonResult(costosPeriodo) { StatusCode = 200 };
        }
    }
}
