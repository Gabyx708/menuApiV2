namespace Domain.Entities
{
    public class Order
    {
        public Guid IdOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public int StateCode { get; set; }
        public State State { get; set; } = null!;
        public string IdUser { get; set; } = null!;
        public User User { get; set; } = null!;
        public Receipt? Receipt { get; set; }
        public Authorization? Authorization { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = null!;
        public virtual ICollection<Transition> Transitions { get; set; } = null!;
    }
}
