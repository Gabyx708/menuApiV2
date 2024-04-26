using Application.Common.Models;
using Application.Interfaces.IOrder;
using Application.UseCase.V2.Order.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICreateOrderCommand _createOrderCommand;
        private readonly ICancelOrderCommand _cancelOrderCommand;

        public OrderController(ICreateOrderCommand createOrderCommand, ICancelOrderCommand cancelOrderCommand)
        {
            _createOrderCommand = createOrderCommand;
            _cancelOrderCommand = cancelOrderCommand;
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

        [HttpPatch("{id}/cancel")]
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

    }
}
