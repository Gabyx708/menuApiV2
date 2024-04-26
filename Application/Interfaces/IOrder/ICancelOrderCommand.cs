using Application.Common.Models;

namespace Application.Interfaces.IOrder
{
    public interface ICancelOrderCommand
    {
        Result<SystemResponse> CancelOrderById(string idOrder);
    }
}
