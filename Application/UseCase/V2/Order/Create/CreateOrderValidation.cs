using Application.Interfaces.IMenu;
using Application.Interfaces.IOrder;
using FluentValidation;

namespace Application.UseCase.V2.Order.Create
{
    internal class CreateOrderValidation : AbstractValidator<CreateOrderRequest>
    {
        private readonly IOrderQuery orderQuery;
        private readonly IMenuQuery menuQuery;

        public CreateOrderValidation(IOrderQuery orderQuery, IMenuQuery menuQuery)
        {
            this.orderQuery = orderQuery;
            this.menuQuery = menuQuery;

            RuleFor(co => co)
                    .Must(request => CheckItemsQuantity(request.Items))
                    .WithMessage("All items must have at least 1 quantity");

            RuleFor(co => co)
                .Must((request) => IsUniqueMenuForUser(request.IdUser, request.IdMenu))
                .WithMessage("A user cannot create 2 orders for the same menu")
                .WithErrorCode("409")
                .Must((request) => CheckMenuDates(request))
                .WithMessage("It is not possible to place an order, this menu has already closed")
                .Must((request) => CheckItemsStock(request.IdMenu, request.Items))
                .WithMessage("There is no stock of food to complete your order");


            RuleFor(co => co.Items)
                    .Must(items => items != null && items.Any())
                    .WithMessage("Your order doesn't have any items");

        }

        private bool IsUniqueMenuForUser(string idUser, Guid idMenu)
        {
            var orders = orderQuery.GetOrdersByMenuAndUser(idMenu, idUser);

            if (orders.Count > 0)
            {
                return false;
            }

            return true;
        }

        private bool CheckItemsQuantity(List<OrderItemRequest> items)
        {
            return items != null && items.All(item => item.Quantity >= 1);
        }

        private bool CheckItemsStock(Guid idMenu,List<OrderItemRequest> items)
        {
            var menu = menuQuery.GetMenuById(idMenu);

            foreach (var item in items)
            {
                int idDish;
                var tryId = int.TryParse(item.IdDish, out idDish);

                var option = menu.Options.FirstOrDefault(o => o.IdDish == idDish);

                bool stockAvailable = (option!.Requested + item.Quantity) >= option.Stock;

                if (option == null || option.Requested == option.Stock || stockAvailable)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckMenuDates(CreateOrderRequest request)
        {
            var menu = menuQuery.GetMenuById(request.IdMenu);
            return menu == null || DateTime.Now <= menu.CloseDate;
        }

    }
}
