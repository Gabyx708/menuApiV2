namespace Domain.Entities
{
    public class MenuOption
    {
        public Guid IdMenu { get; set; }
        public Menu Menu { get; set; } = null!;
        public int IdDish { get; set; }
        public Dish Dish { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Requested { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = null!;
    }
}
