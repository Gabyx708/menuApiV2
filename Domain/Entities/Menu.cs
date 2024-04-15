namespace Domain.Entities
{
    public class Menu
    {
        public Guid IdMenu { get; set; }
        public DateTime FechaConsumo { get; set; }
        public DateTime FechaCarga { get; set; }
        public DateTime FechaCierre { get; set; }

        public IList<MenuPlatillo> MenuPlatillos { get; set; }
    }
}
