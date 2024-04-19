using Application.Interfaces.IAutomation;
using Application.Request.MenuRequests;
using Application.Response.GenericResponses;
using Application.Response.MenuResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _services;
        private readonly IAutomation _automationService;

        public MenuController(IMenuService services, IAutomation automationService)
        {
            _services = services;
            _automationService = automationService;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(MenuResponse), 200)]
        public IActionResult GetMenu(string? id)
        {
            MenuResponse? resultado = null;

            if (id != null)
            {
                resultado = _services.GetMenuById(Guid.Parse(id));
                return new JsonResult(resultado) { StatusCode = 200 };
            }

            resultado = _services.GetUltimoMenu();
            return new JsonResult(resultado) { StatusCode = 200 };
        }



        [HttpPost]
        [ProducesResponseType(typeof(MenuResponse), 201)]
        public IActionResult CreateMenu(MenuRequest request)
        {
            MenuResponse nuevoMenu;

            try
            {
                nuevoMenu = _services.CreateMenu(request);

                //Hacer pedidos automaticos
                _automationService.HacerPedidosAutomatico();

            }
            catch (Exception ex)
            {
                return new JsonResult(new SystemResponse { Message = "error en fechas de menu", StatusCode = 400 }) { StatusCode = 400 };
            }

            return new JsonResult(nuevoMenu) { StatusCode = 201 };
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MenuResponse), 200)]
        public IActionResult DeleteMenu(Guid id)
        {
            var menuBorrado = _services.BorrarMenu(id);
            return new JsonResult(menuBorrado) { StatusCode = 200 };
        }
    }
}
