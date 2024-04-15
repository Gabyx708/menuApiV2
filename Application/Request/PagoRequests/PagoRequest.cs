namespace Application.Request.PagoRequests
{
    public class PagoRequest
    {
        public Guid idPersonal { get; set; }
        public List<Guid> Recibos { get; set; }
    }
}
