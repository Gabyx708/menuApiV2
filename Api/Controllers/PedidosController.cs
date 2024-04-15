using Application.Interfaces.IPedido;
using Application.Response.PedidoResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1.3/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private IPedidoService _services;

        public PedidosController(IPedidoService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<PedidoGetResponse>), 200)]
        public IActionResult ConsultarPedidos(Guid? idPersonal, DateTime? Desde, DateTime? Hasta, int? cantidad)
        {
            var pedidosConsultados = _services.PedidoFiltrado(idPersonal, Desde, Hasta, cantidad);
            return Ok(pedidosConsultados);
        }
    }

}
