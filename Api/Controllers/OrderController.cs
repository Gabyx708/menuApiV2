using Application.Common.Models;
using Application.Interfaces.IOrder;
using Application.UseCase.V2.Order.Create;
using Application.UseCase.V2.Order.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICreateOrderCommand _createOrderCommand;
        private readonly ICancelOrderCommand _cancelOrderCommand;
        private readonly IGetOrderByIdQuery _getOrderByIdQuery;
        private readonly IFinishedOrderCommand _finishedOrderCommand;

        public OrderController(ICreateOrderCommand createOrderCommand,
                               ICancelOrderCommand cancelOrderCommand,
                               IGetOrderByIdQuery getOrderByIdQuery,
                               IFinishedOrderCommand finishedOrderCommand)
        {
            _createOrderCommand = createOrderCommand;
            _cancelOrderCommand = cancelOrderCommand;
            _getOrderByIdQuery = getOrderByIdQuery;
            _finishedOrderCommand = finishedOrderCommand;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderByIdResponse),200)]
        [ProducesResponseType(typeof(SystemResponse), 404)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        public IActionResult GetOrder(string id)
        {
            var result = _getOrderByIdQuery.GetOrderResponseById(id);

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
        [ProducesResponseType(typeof(CreateOrderResponse), 201)]
        [ProducesResponseType(typeof(SystemResponse), 400)]
        [ProducesResponseType(typeof(SystemResponse), 409)]
        public IActionResult CreateNewOrder(CreateOrderRequest request)
        {
            var result = _createOrderCommand.CreateOrder(request);

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
        [HttpPatch("{id}/cancel")]
        [ProducesResponseType(typeof(SystemResponse),200)]
        [ProducesResponseType(typeof(SystemResponse),500)]
        [ProducesResponseType(typeof(SystemResponse),409)]
        public IActionResult CancelOrderById(string id)
        {
            var result = _cancelOrderCommand.CancelOrderById(id);

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

        [Authorize
        [HttpPatch("{id}/finished")]
        [ProducesResponseType(typeof(SystemResponse), 200)]
        [ProducesResponseType(typeof(SystemResponse), 409)]
        [ProducesResponseType(typeof(SystemResponse), 404)]
        public IActionResult FinishedOrder(string id)
        {
            var result = _finishedOrderCommand.ChangeOrderStateToFinished(id);

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
