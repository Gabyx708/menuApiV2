namespace Application.UseCase.V2.Menu.Create
{
    public class CreateMenuRequest
    {
        public DateTime EatingDate { get; set; }
        public DateTime CloseDate { get; set; }
        public List<CreateMenuOptionsRequest> Options { get; set; } = null!;
    }

    public class CreateMenuOptionsRequest
    {
        public int IdDish { get; set; }
        public int Stock { get; set; }
    }
}
