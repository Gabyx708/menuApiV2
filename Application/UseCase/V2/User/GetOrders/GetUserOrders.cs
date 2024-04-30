using Application.Interfaces.IOrder;
using Application.Interfaces.IUser;

namespace Application.UseCase.V2.User.GetOrders
{
    public class GetUserOrders : IGetUserOrdersQuery
    {
        private readonly IOrderQuery orderQuery;
        private readonly IUserQuery userQuery;

        public GetUserOrders(IOrderQuery orderQuery, IUserQuery userQuery)
        {
            this.orderQuery = orderQuery;
            this.userQuery = userQuery;
        }

        public Result<GetUserOrdersResponse> GetOrdersOfUser(string idUser, DateTime? startDate, DateTime? endDate, int index, int quantity)
        {

            var user = userQuery.GetUserById(idUser);

            List<UserOrder> ordersResponses = new();
            var orders = orderQuery.GetOrdersFromUser(idUser, startDate, endDate, index, quantity);

            foreach (var order in orders.Items)
            {
                var orderResponse = new UserOrder
                {
                    Id = order.IdOrder,
                    Date = order.OrderDate,
                    State = order.State.Description,
                    StateCode = order.StateCode,
                };

                ordersResponses.Add(orderResponse);
            }

            var response = new GetUserOrdersResponse
            {
                User = new UserData
                {
                    Id = user.IdUser,
                    Name = $"{user.Name} {user.LastName}"
                },

                Page = new PageOrder
                {
                    Index = orders.PageIndex,
                    TotalPages = orders.TotalPages,
                    TotalOrders = orders.TotalRecords,
                    Orders = ordersResponses
                }
            };

            return Result<GetUserOrdersResponse>.SuccessResult(response);
        }
    }
}
