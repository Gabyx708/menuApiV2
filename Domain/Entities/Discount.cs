namespace Domain.Entities
{
    public class Discount
    {
        public Guid IdDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Percentage { get; set; }
        public ICollection<Receipt> Receipts { get; set; } = null!;
    }
}
