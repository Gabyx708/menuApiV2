using Application.UseCase.V2.Order.GetById;

namespace Application.UseCase.V2.Bills
{
    public class BillsMonthResponse
    {
        public SummaryBill Summary { get; set; } = null!;
        public List<UserSummary> Users { get; set; } = null!;
    }

    public class SummaryBill
    {
        public int year { get; set; }
        public int month { get; set; }
        public string monthName { get; set; } = null!;
        public decimal total { get; set; }
        public decimal totalDiscount { get; set; }
        public OrdersQuantity quantities { get; set; } = null!;
    }

    public class OrdersQuantity
    {
        public int finished { get; set; }
        public int cancelled { get; set; }
        public int inProgress { get; set; }
    }

    public class UserSummary
    {
        public string id { get; set; } = null!;
        public string name { get; set; } = null!;
        public decimal totalSpent { get; set; }
        public decimal totalDiscount { get; set; }
        public List<OrderBillSummary> orders { get; set; } = null!;
    }

    public class OrderBillSummary
    {
        public Guid id { get; set; }
        public DateTime date { get; set; }
        public StateResponse state { get; set; } = null!;
    }
}
