using Application.Common.Models;
using Application.Interfaces.IMenu;
using Application.Response.MenuResponses;
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

        public MenuController(IGetMenuByIdQuery getMenuById, IGetMenuFiltered getMenuFiltered)
        {
            _GetMenuById = getMenuById;
            _GetMenuFiltered = getMenuFiltered;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetMenuByIdResponse), 200)]
        public IActionResult GetMenuById(string id)
        {
            var result = _GetMenuById.GetMenuByIdResponse(id);

            if(result.Success)
            {
                return Ok(result.Data);
            }

            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedListResponse<GetMenuFilterResponse>),200)]
        public IActionResult GetMenuByFiltered(DateTime? initialDate,DateTime? finalDate,int? index)
        {

            var result = _GetMenuFiltered.GetFilterMenuByUploadDate(initialDate,finalDate,index ?? 1);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return StatusCode(result.StatusCode, result.Data);
        }

    }
}
