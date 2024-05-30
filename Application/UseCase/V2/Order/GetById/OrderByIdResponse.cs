namespace Application.UseCase.V2.Order.GetById
{
    public class OrderByIdResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid Menu {get;set;}
        public UserOrderResponse User { get; set; } = null!;
        public List<ITemOrder> Items { get; set; } = null!;
        public StateResponse State { get; set; } = null!;
        public OrderReceiptResponse Receipt { get; set; } = new();
        public List<OrderTransitionResponse> Transitions { get; set; } = null!;
    }

    public class UserOrderResponse
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class ITemOrder
    {
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class StateResponse
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
    }

    public class OrderReceiptResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
    }

    public class OrderTransitionResponse
    {
        public int initialState { get; set; }
        public string initial { get; set; } = null!;
        public int finalState { get; set; }
        public string final { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
