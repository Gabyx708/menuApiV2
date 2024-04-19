namespace Application.UseCase.V2.Dish.Create
{
    public class CreateDishResponse
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
