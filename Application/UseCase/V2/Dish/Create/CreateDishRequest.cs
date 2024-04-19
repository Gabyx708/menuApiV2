namespace Application.UseCase.V2.Dish.Create
{
    public class CreateDishRequest
    {
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
