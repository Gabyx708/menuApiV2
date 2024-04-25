using Application.Interfaces.IOrder;
using Application.Response.GenericResponses;
using Application.UseCase.V2.Order.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICreateOrderCommand _createOrderCommand;

        public OrderController(ICreateOrderCommand createOrderCommand)
        {
            _createOrderCommand = createOrderCommand;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateOrderResponse), 201)]
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

    }
}
