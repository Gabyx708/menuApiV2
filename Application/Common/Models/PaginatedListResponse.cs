namespace Application.Common.Models
{
    public class PaginatedListResponse<T> where T : class
    {
        public int Index { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<T>? Items { get; set; }
    }
}
