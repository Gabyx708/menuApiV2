using Application.Common.Models;
using Application.Interfaces.IMenu;
using Application.Interfaces.IOrder;

namespace Application.UseCase.V2.Order.Cancel
{
    public class CancelOrder : ICancelOrderCommand
    {
        private readonly IOrderCommand orderCommand;
        private readonly IOrderQuery orderQuery;
        private readonly IMenuQuery menuQuery;

        public CancelOrder(IOrderCommand orderCommand,
                           IOrderQuery orderQuery,
                           IMenuQuery menuQuery)
        {
            this.orderCommand = orderCommand;
            this.orderQuery = orderQuery;
            this.menuQuery = menuQuery;
        }

        public Result<SystemResponse> CancelOrderById(string idOrder)
        {

            Guid idOrderCancel;
            Domain.Entities.Order orderToCancel;

            bool isGuid = Guid.TryParse(idOrder, out idOrderCancel);

            if (!isGuid)
            {
                return Result<SystemResponse>.ValidationResult("The order ID must be of type GUID");
            }

            var order = orderQuery.GetOrderById(idOrderCancel);
            DateTime closeDateMenu =  menuQuery.GetMenuById(order.Items.First().MenuOption.IdMenu).CloseDate;

            if (DateTime.Now > closeDateMenu)
            {
                return Result<SystemResponse>.ConflictResult("This menu is closed, you can't cancel your order");
            }

            try
            {
                orderToCancel = orderCommand.CancelOrder(idOrderCancel);
            }
            catch (InvalidOperationException)
            {
                return Result<SystemResponse>.ConflictResult("This order has already been finished or cancelled");
            }
            catch (NullReferenceException)
            {
                return Result<SystemResponse>.NotFoundResult($"The order with id {idOrder} was not found");
            }

            var response = new SystemResponse
            {
                StatusCode = 200,
                Message = $"the order with id {idOrder} has been cancelled"
            };

            return Result<SystemResponse>.SuccessResult(response);
        }
    }
}
