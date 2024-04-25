using Application.UseCase.V2.Order.Create;

namespace Application.Interfaces.IOrder
{
    public interface ICreateOrderCommand
    {
        Result<CreateOrderResponse> CreateOrder(CreateOrderRequest request);
    }
}
