using Application.Interfaces.IBills;
using Application.Interfaces.IOrder;
using Domain.Enums;

namespace Application.UseCase.V2.Bills
{
    public class GetBillsMonth : IGetBillsMonth
    {
        private readonly IOrderQuery orderQuery;

        public GetBillsMonth(IOrderQuery orderQuery)
        {
            this.orderQuery = orderQuery;
        }

        public Result<BillsMonthResponse> GetBillsInMonth(int year, int month)
        {
            var orders = orderQuery.GetOrdesInMonth(year, month);
            var orderUsersGroup = orders.Where(o => o.StateCode == (int)OrderState.Finished)
                    .GroupBy(o => o.User).ToList();

            List<UserSummary> users = new();
            decimal total = 0;
            decimal totalDiscount = 0;
            int finished = 0;
            int cancelled = 0;
            int inProgress = 0;

            foreach (var order in orders)
            {
                if (order.State.StateCode == (int)OrderState.Finished)
                {
                    total += order.Receipt!.TotalPrice;
                    totalDiscount += order.Receipt!.TotalPrice * (order.Receipt.Discount.Percentage / 100);
                    finished++;
                };

                if (order.State.StateCode == (int)OrderState.Cancelled) { cancelled++; };
                if (order.State.StateCode == (int)OrderState.InProgress) { inProgress++; };

            }


            foreach (var userGroup in orderUsersGroup)
            {
                decimal userTotal = 0;
                decimal userTotalDiscount = 0;

                List<OrderBillSummary> userOrders = new();

                foreach (var order in userGroup)
                {
                    userTotal += order.Receipt!.TotalPrice;
                    userTotalDiscount += order.Receipt.TotalPrice * (order.Receipt.Discount.Percentage / 100);

                    var orderSummary = new OrderBillSummary
                    {
                        id = order.IdOrder,
                        date = order.OrderDate,
                        state = new Order.GetById.StateResponse
                        {
                            Id = order.State.StateCode,
                            Description = order.State.Description
                        }
                    };

                    userOrders.Add(orderSummary);
                }

                var userSummary = new UserSummary
                {
                    id = userGroup.Key.IdUser,
                    name = $"{userGroup.Key.Name} {userGroup.Key.LastName}",
                    totalSpent = userTotal,
                    totalDiscount = userTotalDiscount,
                    orders = userOrders
                };

                users.Add(userSummary);
            }


            var quantities = new OrdersQuantity
            {
                finished = finished,
                cancelled = cancelled,
                inProgress = inProgress
            };

            var summary = new SummaryBill
            {
                year = year,
                month = month,
                monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                total = total,
                totalDiscount = totalDiscount,
                quantities = quantities
            };

            var response = new BillsMonthResponse
            {
                Summary = summary,
                Users = users
            };

            return Result<BillsMonthResponse>.SuccessResult(response);
        }

    }
}
