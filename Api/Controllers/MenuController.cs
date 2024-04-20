using Application.Common.Models;
using Application.Interfaces.IMenu;
using Application.UseCase.V2.Menu.Create;
using Application.UseCase.V2.Menu.GetById;
using Application.UseCase.V2.Menu.GetFilter;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IGetMenuByIdQuery _GetMenuById;
        private readonly IGetMenuFiltered _GetMenuFiltered;
        private readonly ICreateMenuCommand _CreateMenuCommand;

        public MenuController(IGetMenuByIdQuery getMenuById,
                              IGetMenuFiltered getMenuFiltered,
                              ICreateMenuCommand createMenuCommand)
        {
            _GetMenuById = getMenuById;
            _GetMenuFiltered = getMenuFiltered;
            _CreateMenuCommand = createMenuCommand;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetMenuByIdResponse), 200)]
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

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedListResponse<GetMenuFilterResponse>), 200)]
        public IActionResult GetMenuByFiltered(DateTime? initialDate, DateTime? finalDate, int? index)
        {
            var result = _GetMenuFiltered.GetFilterMenuByUploadDate(initialDate, finalDate, index ?? 1);

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

        [HttpPost]
        [ProducesResponseType(typeof(CreateMenuResponse), 201)]
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

    }
}
