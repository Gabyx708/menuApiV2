namespace Domain.Entities
{
    public class Menu
    {
        public Guid IdMenu { get; set; }
        public DateTime EatingDate { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime CloseDate { get; set; }

        public ICollection<MenuOption> Options { get; set; } = null!;
    }
}
