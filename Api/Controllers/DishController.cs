using Application.Common.Models;
using Application.Interfaces.IDish;
using Application.UseCase.V2.Dish.Create;
using Application.UseCase.V2.Dish.GetByDescription;
using Application.UseCase.V2.Dish.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ICreateDishCommand _createDishCommand;
        private readonly IGetDishesByDescription _getDishByDescription;
        private readonly IGetDishByIdQuery _getDishesById;
        private readonly IDishesUpdatePrice _updateDishesPrice;

        public DishController(ICreateDishCommand createDishCommand,
                             IGetDishesByDescription getDishByDescription,
                             IGetDishByIdQuery getDishesById,
                             IDishesUpdatePrice updateDishesPrice)
        {
            _createDishCommand = createDishCommand;
            _getDishByDescription = getDishByDescription;
            _getDishesById = getDishesById;
            _updateDishesPrice = updateDishesPrice;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateDishResponse), 201)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        [ProducesResponseType(typeof(SystemResponse), 500)]
        public IActionResult CreateNewDish(CreateDishRequest request)
        {
            var result = _createDishCommand.CreateDish(request);

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

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedListResponse<GetDishResponse>), 200)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        [ProducesResponseType(typeof(SystemResponse), 500)]
        public IActionResult GetDishesByDescription(string? description,int? index,int? quantity)
        {
            var result = _getDishByDescription.GetDishesByDescription(description ?? "", index ?? 1, quantity ?? 10);

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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetDishByIdResponse), 200)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        [ProducesResponseType(typeof(SystemResponse), 404)]
        public IActionResult GetDishById(string id)
        {
            var result = _getDishesById.GetDishById(id);

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
        [HttpPost("update-prices/{price}")]
        [ProducesResponseType(typeof(SystemResponse),200)]
        public IActionResult UpdateAllDishesPrice(decimal price)
        {
            var result = _updateDishesPrice.UpdateDishesPrices(price);

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
