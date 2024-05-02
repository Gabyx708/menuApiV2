using Application.Common.Models;
using Application.Interfaces.IMenu;
using Application.UseCase.V2.Menu.Create;
using Application.UseCase.V2.Menu.GetById;
using Application.UseCase.V2.Menu.GetFilter;
using Application.UseCase.V2.Menu.GetNextAvailable;
using Application.UseCase.V2.Menu.GetWithOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IGetMenuByIdQuery _GetMenuById;
        private readonly IGetMenuFiltered _GetMenuFiltered;
        private readonly IGetNextMenuAvailable _GetNextMenu;
        private readonly IGetMenuWithOrders _GetMenuWithOrders;
        private readonly ICreateMenuCommand _CreateMenuCommand;

        public MenuController(IGetMenuByIdQuery getMenuById,
                              IGetMenuFiltered getMenuFiltered,
                              ICreateMenuCommand createMenuCommand,
                              IGetNextMenuAvailable getNextMenu,
                              IGetMenuWithOrders getMenuWithOrders)
        {
            _GetMenuById = getMenuById;
            _GetMenuFiltered = getMenuFiltered;
            _CreateMenuCommand = createMenuCommand;
            _GetNextMenu = getNextMenu;
            _GetMenuWithOrders = getMenuWithOrders;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetMenuByIdResponse), 200)]
        [ProducesResponseType(typeof(SystemResponse), 404)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        public IActionResult GetMenuById(string id)
        {
            var result = _GetMenuById.GetMenuByIdResponse(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedListResponse<GetMenuFilterResponse>), 200)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        public IActionResult GetMenuByFiltered(DateTime? initialDate, DateTime? finalDate, int? index, int? quantity)
        {
            var result = _GetMenuFiltered.GetFilterMenuByEatingDate(initialDate, finalDate, quantity ?? 10, index ?? 1);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateMenuResponse), 201)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        [ProducesResponseType(typeof(SystemResponse), 409)]
        public IActionResult CreateNewMenu(CreateMenuRequest request)
        {
            var result = _CreateMenuCommand.CreateMenu(request);

            if (result.Success)
            {
                return Created("", result.Data);
            }

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };
        }

        [Authorize]
        [HttpGet("next-available")]
        [ProducesResponseType(typeof(GetNextMenuResponse), 200)]
        [ProducesResponseType(typeof(SystemResponse), 500)]
        public IActionResult NextMenuAvailble()
        {
            var result = _GetNextMenu.GetNextMenuAvailableForUsers();

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };
        }

        [HttpGet("{id}/orders")]
        [ProducesResponseType(typeof(MenuWithOrdersResponse), 200)]
        public IActionResult GetMenuByIdWithAllOrders(Guid id)
        {
            var result = _GetMenuWithOrders.GetMenuWithAllOrders(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };
        }

    }
}
