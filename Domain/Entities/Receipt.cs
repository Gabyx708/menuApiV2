namespace Domain.Entities
{
    public class Receipt
    {
        public Guid IdReceipt { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }

        public Guid IdOrder { get; set; }
        public Order Order { get; set; } = null!;
        public Guid IdDiscount { get; set; }
        public Discount Discount { get; set; } = null!;

    }
}
