namespace Application.Response.PersonalResponses
{
    public class PersonalResponse
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public string dni { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public DateTime fecha_alta { get; set; }
        public DateTime fecha_ingreso { get; set; }

        public string mail { get; set; }
        public string telefono { get; set; }
        public bool isAutomatico { get; set; }
    }
}
