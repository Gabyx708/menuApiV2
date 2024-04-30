namespace Application.UseCase.V2.User.GetOrders
{
    public class GetUserOrdersResponse
    {
        public UserData User { get; set; } = null!;
        public PageOrder Page { get; set; } = null!;
    }

    public class UserData
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

    public class UserOrder
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string State { get; set; } = null!;
        public int StateCode { get; set; }
    }

    public class PageOrder
    {
        public int Index { get; set; }
        public int TotalPages { get; set; }
        public int TotalOrders { get; set; }
        public List<UserOrder> Orders { get; set; } = new();
    }
}
