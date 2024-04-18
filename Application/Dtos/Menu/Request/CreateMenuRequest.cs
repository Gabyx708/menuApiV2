namespace Application.Dtos.Menu.Request
{
    public class CreateMenuRequest
    {
        public DateTime EatingDate { get; set; }
        public DateTime CloseDate { get; set; }
        public List<CreateMenuOptionsRequest> Options { get; set; } = null!;
    }
}
