using Application.Interfaces.IPagos;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class PagoQuery : IPagoQuery
    {
        private readonly MenuAppContext _context;

        public PagoQuery(MenuAppContext context)
        {
            _context = context;
        }

        public List<Pago> GetAllPagos()
        {
            return _context.Pagos.ToList();
        }

        public Pago GetPagoById(long NPago)
        {
            var pago = _context.Pagos.FirstOrDefault(p => p.NumeroPago == NPago);

            if (pago == null) { return null; }

            pago.Recibos = _context.Recibos.Where(r => r.NumeroPago == NPago).ToList();

            return pago;
        }

        public List<Pago> GetPagoFiltrado(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Pago> pagosFiltrados = _context.Pagos.Where(p => p.FechaPago.Date >= fechaDesde.Date && p.FechaPago <= fechaHasta.Date)
                                        .ToList();

            return pagosFiltrados;
        }
    }
}
