using Application.Interfaces.IOrder;
using Application.Interfaces.IUser;

namespace Application.UseCase.V2.User.GetBills
{
    public class GetUserBills : IGetUserBills
    {
        private readonly IOrderQuery orderQuery;
        private readonly IUserQuery userQuery;

        public GetUserBills(IOrderQuery orderQuery, IUserQuery userQuery)
        {
            this.orderQuery = orderQuery;
            this.userQuery = userQuery;
        }

        public Result<UserBillResponse> GetMonthBill(string idUser, int year, int month)
        {
            var user = userQuery.GetUserById(idUser);

            if (user == null) return Result<UserBillResponse>.NotFoundResult("user not found");

            string userName = $"{user.Name} {user.LastName}";

            var orders = orderQuery.GetOrderByUserInMonth(idUser, year, month);
            List<OrdersReceipts> ordersResponses = new();

            decimal total = 0;
            decimal totalDiscount = 0;
            int quantity = 0;

            foreach (var order in orders)
            {
                var orderAux = new OrdersReceipts
                {
                    idOrder = order.IdOrder,
                    idReceipts = order.Receipt!.IdReceipt,
                    discount = order.Receipt.Discount.Percentage
                };

                total += order.Receipt.TotalPrice;
                totalDiscount += order.Receipt.TotalPrice * (order.Receipt.Discount.Percentage / 100);

                quantity++;

                ordersResponses.Add(orderAux);
            }

            var response = new UserBillResponse
            {
                User = userName,
                Total = total,
                OrdersQuantity = quantity,
                Month = new MonthInfo
                {
                    month = month,
                    year = year,
                    monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)
                },
                TotalDiscount = totalDiscount,
                orders = ordersResponses
            };

            return Result<UserBillResponse>.SuccessResult(response);
        }
    }
}
