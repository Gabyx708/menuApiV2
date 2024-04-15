namespace Application.Request.PersonalRequests
{
    public class PersonalRequest
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string dni { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string mail { get; set; }
        public string telefono { get; set; }
        public int privilegio { get; set; }
    }
}
