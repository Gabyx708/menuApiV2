using Application.UseCase.V2.Order.GetById;

namespace Application.Interfaces.IOrder
{
    public interface IGetOrderByIdQuery
    {
        Result<OrderByIdResponse> GetOrderResponseById(string idOrder);
    }
}
