namespace Domain.Entities
{
    public class State
    {
        public int StateCode { get; set; }
        public string Description { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
        public ICollection<Transition> InitialTransitions { get; set; } = null!;
        public ICollection<Transition> FinalTransitions { get; set; } = null!;
    }
}
