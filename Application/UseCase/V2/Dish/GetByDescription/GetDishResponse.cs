namespace Application.UseCase.V2.Dish.GetByDescription
{
    public class GetDishResponse
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
