using Application.Common.Models;

namespace Application.Interfaces.IOrder
{
    public interface IFinishedOrderCommand
    {
        Result<SystemResponse> ChangeOrderStateToFinished(string id);
    }
}
