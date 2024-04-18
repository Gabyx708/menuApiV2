namespace Domain.Entities
{
    public class Order
    {
        public Guid IdOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public int StateCode { get; set; }
        public State State { get; set; } = null!;
        public Guid IdUser { get; set; }
        public User User { get; set; } = null!;
        public Receipt? Receipt { get; set; }
        public Authorization? Authorization { get; set; }

        public ICollection<OrderItem> Items { get; set; } = null!;
        public ICollection<Transition> Transitions { get; set; } = null!;
    }
}
