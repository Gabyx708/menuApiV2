using Application.Interfaces.IMenu;
using Application.Interfaces.IOrder;
using Application.UseCase.V2.Order.GetById;

namespace Application.UseCase.V2.Menu.GetUserOrderFromMenu
{
    public class GetUserOrderFromMenu : IGetUserOrdersFromMenu
    {
        private readonly IOrderQuery orderQuery;

        public GetUserOrderFromMenu(IOrderQuery orderQuery)
        {
            this.orderQuery = orderQuery;
        }

        public Result<List<UserOrderFromMenu>> GetUserOrdersFromMenu(string idMenu, string idUser)
        {
            Guid menuId;
            bool isGuid = Guid.TryParse(idMenu, out menuId);

            if (!isGuid)
            {
                return Result<List<UserOrderFromMenu>>.ValidationResult("the id type must be uid");
            }

            var ordersToMap = orderQuery.GetOrdersByMenuAndUser(menuId, idUser);

            if (ordersToMap.Count == 0)
            {
                return Result<List<UserOrderFromMenu>>.NotFoundResult("orders not found");
            }

            List<UserOrderFromMenu> ordersResponses = new();

            foreach (var order in ordersToMap)
            {
                var orderResponse = new UserOrderFromMenu
                {
                    Id = order.IdOrder,
                    Date = order.OrderDate,
                    User = new UserOrderResponse { Id = order.User.IdUser, Name = $"{order.User.Name} {order.User.LastName}" },
                    Items = this.GetItemOrder(order),
                    State = new StateResponse { Id = order.State.StateCode, Description = order.State.Description },
                    Transitions = this.GetOrderTransitions(order),
                    Receipt = this.GetReceipt(order)
                };

                ordersResponses.Add(orderResponse);
            }

            return Result<List<UserOrderFromMenu>>.SuccessResult(ordersResponses);
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
