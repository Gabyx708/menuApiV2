using Application.Exceptions;
using Application.Interfaces.IPedido;
using Application.Request.PedidoRequests;
using Application.Response.GenericResponses;
using Application.Response.PedidoResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IPedidoService _services;

        public OrderController(IPedidoService services)
        {
            _services = services;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(PedidoResponse), 201)]
        public IActionResult HacerUnPedido(PedidoRequest request)
        {
            PedidoResponse result = new PedidoResponse();
            //TODO refactorizar 
            var usuarioActual = HttpContext.User;
            var usuarioRol = usuarioActual.Claims.FirstOrDefault(u => u.Type == "rol").Value;
            var usuarioId = usuarioActual.Claims.FirstOrDefault(u => u.Type == "id").Value;
            var rolAdministrador = "1";

            try
            {

                if (usuarioRol == rolAdministrador && usuarioRol != null)
                {

                    result = _services.HacerUnpedidoSinRestricciones(request, new Guid(usuarioId));
                }
                else
                {
                    result = _services.HacerUnpedido(request);
                }

                return new JsonResult(result) { StatusCode = 201 };
            }
            catch (SystemExceptionApp e)
            {
                return new JsonResult(e._response) { StatusCode = e._response.StatusCode };
            }
            catch (Exception e)
            {
                return new JsonResult(new SystemResponse { Message = "falla interna", StatusCode = 500 }) { StatusCode = 500 };
            }

        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PedidoResponse), 200)]
        public IActionResult ConsularPedido(Guid id)
        {
            var pedidoConsultado = _services.GetPedidoById(id);
            return Ok(pedidoConsultado);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeletePedido(Guid id)
        {
            PedidoResponse pedidoEliminado = new PedidoResponse();
            try
            {
                pedidoEliminado = _services.EliminarPedido(id);
            }
            catch (InvalidOperationException)
            {
                return new JsonResult(new SystemResponse { Message = "fecha excedida", StatusCode = 409 }) { StatusCode = 409 };
            }

            return new JsonResult(pedidoEliminado);
        }

    }
}
