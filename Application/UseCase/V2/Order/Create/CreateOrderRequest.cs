namespace Application.UseCase.V2.Order.Create
{
    public class CreateOrderRequest
    {
        public string IdUser { get; set; } = null!;
        public Guid IdMenu { get; set; }
        public List<OrderItemRequest> Items { get; set; } = null!;
    }

    public class OrderItemRequest
    {
        public string IdDish { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
