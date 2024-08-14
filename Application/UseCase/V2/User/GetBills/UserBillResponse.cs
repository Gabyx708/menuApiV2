namespace Application.UseCase.V2.User.GetBills
{
    public class UserBillResponse
    {
        public string User { get; set; } = null!;
        public MonthInfo Month { get; set; } = null!;
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
        public int OrdersQuantity { get; set; }
        public List<OrdersReceipts> orders { get; set; } = null!;
    }

    public class OrdersReceipts
    {
        public Guid idOrder { get; set; }
        public Guid idReceipts { get; set; }
        public decimal discount { get; set; }
    }

    public class MonthInfo
    {
        public int year { get;set; }
        public int month { get;set; }
        public string monthName { get; set; } = null!;
    }
}
