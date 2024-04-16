namespace Domain.Entities
{
    public class Authorization
    {
        public Guid IdOrder { get; set; }
        public Guid IdUser { get; set; }
        public string Reason { get; set; } = null!;
        public Order Order { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
