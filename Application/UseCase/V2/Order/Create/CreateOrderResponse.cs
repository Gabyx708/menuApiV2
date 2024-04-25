namespace Application.UseCase.V2.Order.Create
{
    public class CreateOrderResponse
    {
        public Guid Id { get; set; }
        public string User { get; set; } = null!;
        public string State { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; } = null!;
    }

    public class OrderItemResponse
    {
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
