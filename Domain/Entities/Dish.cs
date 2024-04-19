namespace Domain.Entities
{
    public class Dish
    {
        public int IdDish { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public bool Activated { get; set; }

    }
}
