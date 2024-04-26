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

        public CancelOrder(IOrderCommand orderCommand, IOrderQuery orderQuery, IMenuQuery menuQuery)
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
            

            Guid idMenu = orderQuery.GetOrderById(idOrderCancel).Items.First().IdMenu;

            var menu = menuQuery.GetMenuById(idMenu);

            try
            {
                orderToCancel = orderCommand.CancelOrder(idOrderCancel);
            }
            catch (InvalidOperationException)
            {
                return Result<SystemResponse>.ConflictResult("This order has already been finished or cancelled");
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
