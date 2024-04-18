namespace Domain.Entities
{
    public class OrderItem
    {
        public Guid IdOrder { get; set; }
        public Order Order { get; set; } = null!;

        public Guid IdMenu { get; set; }
        public Menu Menu { get; set; } = null!;

        public int IdDish { get; set; }
        public Dish Dish { get; set; } = null!;

        public int Quantity { get; set; }
        public MenuOption MenuOption { get; set; } = null!;
    }
}
