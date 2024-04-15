namespace Domain.Entities
{
    public class Pago
    {
        public long NumeroPago { get; set; }
        public Guid idPersonal { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoPagado { get; set; }
        public bool IsAnulado { get; set; }

        public ICollection<Recibo> Recibos { get; set; }
        public Personal Personal { get; set; }
    }
}
