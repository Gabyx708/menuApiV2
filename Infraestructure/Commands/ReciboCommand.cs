using Application.Interfaces.IRecibo;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class ReciboCommand : IReciboCommand
    {
        private readonly MenuAppContext _context;

        public ReciboCommand(MenuAppContext context)
        {
            _context = context;
        }

        public Recibo CambiarPrecioTotal(Guid idRecibo, decimal precioTotal)
        {
            var found = _context.Recibos.FirstOrDefault(r => r.IdRecibo == idRecibo);

            if (found == null) { return null; }

            found.IdRecibo = found.IdRecibo;
            found.IdDescuento = found.IdDescuento;
            found.precioTotal = precioTotal;

            _context.Update(found);
            _context.SaveChanges();
            return found;
        }

        public Recibo CrearRecibo(Recibo ReciboNuevo)
        {
            _context.Add(ReciboNuevo);
            _context.SaveChanges();
            return ReciboNuevo;
        }

        public Recibo EliminarRecibo(Guid idRecibo)
        {
            var found = _context.Recibos.FirstOrDefault(r => r.IdRecibo == idRecibo);

            if (found != null)
            {
                _context.Remove(found);
                _context.SaveChanges();
                return found;
            }

            return null;
        }
    }
}
