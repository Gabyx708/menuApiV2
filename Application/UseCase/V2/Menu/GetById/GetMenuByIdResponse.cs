namespace Application.UseCase.V2.Menu.GetById
{
    public class GetMenuByIdResponse
    {
        public Guid Id { get; set; }
        public DateTime EatingDate { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime CloseDate { get; set; }
        public List<OptionResponse> Options { get; set; } = null!;
    }

    public class OptionResponse
    {
        public int idDish { get; set; }
        public string description { get; set; } = null!;
    }
}
