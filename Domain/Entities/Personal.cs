namespace Domain.Entities
{
    public class Personal
    {
        public Guid IdPersonal { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Dni { get; set; }
        public DateTime FechaNac { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaIngreso { get; set; }

        public string Mail { get; set; }
        public string Telefono { get; set; }
        public int Privilegio { get; set; }
        public string Password { get; set; }
        public bool isAutomatico { get; set; }

        public ICollection<Pedido> Pedidos { get; set; }
        public ICollection<Pago> pagos { get; set; }
    }
}
