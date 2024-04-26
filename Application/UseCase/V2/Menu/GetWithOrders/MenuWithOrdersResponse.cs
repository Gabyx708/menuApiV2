namespace Application.UseCase.V2.Menu.GetWithOrders
{
    public class MenuWithOrdersResponse
    {
        public MenuData Data { get; set; } = null!;
        public List<SummaryOrder> Orders { get; set; } = null!;
    }

    public class MenuData
    {
        public Guid Id { get; set; }
        public DateTime EatingDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime UploadDate { get; set; }
        public int TotalOrders { get; set; }
        public  int InProgress { get; set; }
        public int CancelledOrders { get; set; }
        public int FinishedOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalAllOrders { get; set; }
    }

    public class SummaryOrder
    {

        public Guid Id { get; set; }
        public string IdUser { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string State { get; set; } = null!;
        public List<SummaryOrderItemResponse> Items { get; set; } = null!;
    }

    public class SummaryOrderItemResponse
    {
        public int IdDish { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
