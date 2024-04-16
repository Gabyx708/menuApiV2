namespace Domain.Entities
{
    public class MenuOption
    {
        public Guid IdMenu { get; set; }
        public int IdDish { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Requested { get; set; }
    }
}
