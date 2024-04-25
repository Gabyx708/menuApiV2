using Application.Interfaces.I;
using Application.Interfaces.IMenu;
using Application.Interfaces.IOrder;
using Application.Interfaces.IUnitOfWork;
using Domain.Entities;
using Domain.Enums;

namespace Application.UseCase.V2.Order.Create
{
    public class CreateOrderCommand : ICreateOrderCommand
    {
        private readonly IMenuQuery menuQuery;
        private readonly IOrderQuery orderQuery;
        private readonly IMenuOptionQuery menuOptionQuery;
        private readonly IUnitOfWorkCreateOrder _unitOfWorkCreateOrder;

        public CreateOrderCommand(IMenuQuery menuQuery,
                                  IOrderQuery orderQuery,
                                  IMenuOptionQuery menuOptionQuery,
                                  IUnitOfWorkCreateOrder unitOfWorkCreateOrder)
        {
            this.menuQuery = menuQuery;
            this.orderQuery = orderQuery;
            _unitOfWorkCreateOrder = unitOfWorkCreateOrder;
            this.menuOptionQuery = menuOptionQuery;
        }

        public Result<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
        {
            var validator = new CreateOrderValidation(orderQuery, menuQuery);
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                var conflictError = validationResult.Errors.FirstOrDefault(e => e.ErrorCode == "409");

                if (conflictError != null)
                {
                    return Result<CreateOrderResponse>.ConflictResult(errorMessages);
                }

                return Result<CreateOrderResponse>.ValidationResult(errorMessages);
            }

            var newOrder = this.GenerateOrder(request);
            bool updateOptionSuccess = this.UpdateMenuOptionStock(request.Items, request.IdMenu);

            _unitOfWorkCreateOrder.OrderCommand.InsertOrder(newOrder);

            _unitOfWorkCreateOrder.Save();

            var orderRecovered = orderQuery.GetOrderById(newOrder.IdOrder);

            return Result<CreateOrderResponse>.SuccessResult(this.GenerateResponse(orderRecovered));
        }

        private Domain.Entities.Order GenerateOrder(CreateOrderRequest request)
        {
            var itemsOrder = new List<OrderItem>();

            foreach (var item in request.Items)
            {
                var orderItem = new OrderItem
                {
                    IdDish = int.Parse(item.IdDish),
                    IdMenu = request.IdMenu,
                    Quantity = item.Quantity
                };

                itemsOrder.Add(orderItem);
            }
            return new Domain.Entities.Order
            {
                OrderDate = DateTime.Now,
                StateCode = (int)OrderState.InProgress,
                IdUser = request.IdUser,
                Items = itemsOrder
            };
        }

        private bool UpdateMenuOptionStock(List<OrderItemRequest> items, Guid idMenu)
        {
            try
            {
                foreach (var option in items)
                {
                    var originalOption = menuOptionQuery.GetById(idMenu, int.Parse(option.IdDish));

                    originalOption.Requested = (originalOption.Requested) + option.Quantity;
                    _unitOfWorkCreateOrder.MenuOptionCommand.UpdateMenuOption(originalOption);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private CreateOrderResponse GenerateResponse(Domain.Entities.Order orderRecovered)
        {
            decimal totalPrice = orderRecovered.Items.Sum(item => item.Quantity * item.MenuOption.Price);
            List<OrderItemResponse> itemsResponses = new();

            foreach (var orderItem in orderRecovered.Items)
            {
                var itemResponse = new OrderItemResponse
                {
                    Description = orderItem.MenuOption.Dish.Description,
                    Quantity = orderItem.Quantity,
                    Price = orderItem.MenuOption.Price
                };

                itemsResponses.Add(itemResponse);
            }

            if (itemsResponses.Count <= 0)
            {
                throw new InvalidOperationException();
            }

            var orderResponse = new CreateOrderResponse
            {
                Id = orderRecovered.IdOrder,
                User = $"{orderRecovered.User.Name} {orderRecovered.User.LastName}",
                State = $"{orderRecovered.State.Description}",
                OrderItems = itemsResponses,
                TotalPrice = totalPrice
            };

            return orderResponse;
        }
    }
}
