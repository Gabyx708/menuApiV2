using Application.Interfaces.IOrder;

namespace Application.UseCase.V2.Order.GetById
{
    public class GetOrderById : IGetOrderByIdQuery
    {
        private readonly IOrderQuery orderQuery;

        public GetOrderById(IOrderQuery orderQuery)
        {
            this.orderQuery = orderQuery;
        }

        public Result<OrderByIdResponse> GetOrderResponseById(string idOrder)
        {
            Guid id;
            bool isGuid = Guid.TryParse(idOrder, out id);
            Domain.Entities.Order order;

            if (!isGuid)
            {
                return Result<OrderByIdResponse>.ValidationResult($"The ID format must be of type GUID");
            }

            try
            {
                order = orderQuery.GetOrderById(id);
            }
            catch (NullReferenceException)
            {
                return Result<OrderByIdResponse>.NotFoundResult($"The order with the ID was not found: {idOrder}");
            }

            var items = this.GetItemOrder(order);
            var transitions = this.GetOrderTransitions(order);
            var receipt = this.GetReceipt(order);

            var orderResponse = new OrderByIdResponse
            {
                Id = order.IdOrder,
                Date = order.OrderDate,
                User = new UserOrderResponse { Id = order.User.IdUser, Name = $"{order.User.Name} {order.User.LastName}" },
                Items = items,
                State = new StateResponse { Id = order.State.StateCode, Description = order.State.Description },
                Transitions =transitions,
                Receipt = receipt
            };

            return Result<OrderByIdResponse>.SuccessResult(orderResponse);
        }

        private List<ITemOrder> GetItemOrder(Domain.Entities.Order order)
        {
            var itemsResponses = new List<ITemOrder>();

            foreach (var item in order.Items)
            {
                var itemResponse = new ITemOrder
                {
                    Description = item.MenuOption.Dish.Description,
                    Price = item.MenuOption.Price,
                    Quantity = item.Quantity
                };

                itemsResponses.Add(itemResponse);
            }

            return itemsResponses;
        }

        private List<OrderTransitionResponse> GetOrderTransitions(Domain.Entities.Order order)
        {
            if (order.Transitions == null)
            {
                return null!;
            }

            var transitionsResponses = new List<OrderTransitionResponse>();

            foreach (var transition in order.Transitions)
            {
                var transitionResponse = new OrderTransitionResponse
                {
                    initialState = transition.InitialStateCode,
                    initial = transition.InitialState.Description,
                    finalState = transition.FinalStateCode,
                    final = transition.FinalSate.Description,
                    Date = transition.Date
                };

                transitionsResponses.Add(transitionResponse);
            }

            return transitionsResponses;
        }

        private OrderReceiptResponse GetReceipt(Domain.Entities.Order order)
        {
            if (order.Receipt == null)
            {
                return null!;
            }

            decimal totalPrice = order.Items.Sum(item => item.Quantity * item.MenuOption.Price);
            decimal totalDiscount = (order.Receipt.Discount.Percentage / 100) * totalPrice;

            return new OrderReceiptResponse
            {
                Id = order.Receipt.IdReceipt,
                Date = order.Receipt.Date,
                TotalPrice = totalPrice,
                TotalDiscount = totalDiscount
            };
        }
    }
}
