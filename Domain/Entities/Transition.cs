namespace Domain.Entities
{
    public class Transition
    {
        public Guid IdOrder { get; set; }
        public Order Order { get; set; } = null!;
        public int InitialStateCode { get; set; }
        public State InitialState { get; set; } = null!;
        public int FinalStateCode { get; set; }
        public State FinalSate { get; set; } = null!;
        public DateTime Date {  get; set; }
    }
}
