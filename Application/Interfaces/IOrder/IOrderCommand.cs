using Domain.Entities;

namespace Application.Interfaces.IOrder
{
    public interface IOrderCommand
    {
        Order InsertOrder(Order order);
        Order CancelOrder(Guid idOrder);
        Order ChangeOrderState(Guid idOrder, int idState);
    }
}
