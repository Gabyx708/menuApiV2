using Application.UseCase.V2.User.GetOrders;

namespace Application.Interfaces.IUser
{
    public interface IGetUserOrdersQuery
    {
        Result<GetUserOrdersResponse> GetOrdersOfUser(string idUser, DateTime? startDate, DateTime? endDate, int index, int quantity);
    }
}
