using Application.Common.Models;
using Application.Interfaces.IDiscount;
using Application.Interfaces.IOrder;
using Application.Interfaces.IUnitOfWork;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCase.V2.Order.Finished
{
    public class FinishedOrderCommand : IFinishedOrderCommand
    {
        private readonly IOrderQuery orderQuery;
        private readonly IDiscountQuery discountQuery;
        private readonly IUnitOfWorkFinishedOrder _unitOfWorkFinishedOrder;

        public FinishedOrderCommand(IOrderQuery orderQuery,
                                    IDiscountQuery discountQuery,
                                    IUnitOfWorkFinishedOrder unitOfWorkFinishedOrder)
        {
            this.orderQuery = orderQuery;
            this.discountQuery = discountQuery;
            _unitOfWorkFinishedOrder = unitOfWorkFinishedOrder;
        }

        public Result<SystemResponse> ChangeOrderStateToFinished(string id)
        {
            int finishedState = (int)OrderState.Finished;
            Guid idOrder;

            Guid.TryParse(id, out idOrder);

            var order = orderQuery.GetOrderById(idOrder);

            try
            {
                _unitOfWorkFinishedOrder.OrderCommand.ChangeOrderState(idOrder, finishedState);
            }
            catch (NullReferenceException)
            {
                return Result<SystemResponse>.NotFoundResult($"order not found: {idOrder}");
            }
            catch (InvalidOperationException)
            {
                return Result<SystemResponse>.ConflictResult("This order cannot be finalized");
            }


            decimal totalPrice = order.Items.Sum(item => item.Quantity * item.MenuOption.Price);

            Guid idDiscount = discountQuery.GetLastestDiscount().IdDiscount;

            var receipt = new Receipt
            {
                Date = DateTime.Now,
                IdOrder = order.IdOrder,
                IdDiscount = idDiscount,
                TotalPrice = totalPrice
            };

            _unitOfWorkFinishedOrder.ReceiptCommand.InsertReceipt(receipt);

            _unitOfWorkFinishedOrder.Save();

            var response = new SystemResponse
            {
                StatusCode = 200,
                Message = $"The order with ID: {idOrder} was finalized and a receipt was generated"
            };

            return Result<SystemResponse>.SuccessResult(response);
        }
    }
}
