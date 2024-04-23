using Application.Interfaces.IOrder;
using FluentValidation;

namespace Application.UseCase.V2.Order.Create
{
    internal class CreateOrderValidation : AbstractValidator<CreateOrderRequest>
    {
        private readonly IOrderQuery orderQuery;
        private string ExistOrderMessage { get; set; } = string.Empty;

        public CreateOrderValidation(IOrderQuery orderQuery)
        {
            this.orderQuery = orderQuery;

            RuleFor(co => co.IdMenu)
                .NotEmpty().WithMessage("the menu ID is required.")
                .Must((request, userId) => BeUniqueMenuForUser(request.IdUser, request.IdMenu))
                .WithMessage(this.ExistOrderMessage);
        }

        private bool BeUniqueMenuForUser(string idUser, Guid idMenu)
        {
            var orders = orderQuery.GetOrdersByMenuAndUser(idMenu, idUser);

            if (orders.Count > 0)
            {
                ExistOrderMessage = "exits";
                return false;
            }

            return true;
        }

    }
}
