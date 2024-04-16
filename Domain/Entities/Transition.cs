namespace Domain.Entities
{
    public class Transition
    {
        public Guid IdOrder { get; set; }
        public int InitialState { get; set; }
        public int FinalState { get; set; }
        public DateTime Date {  get; set; }
    }
}
