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

        public Receipt CambiarPrecioTotal(Guid idRecibo, decimal precioTotal)
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

        public Receipt CrearRecibo(Receipt ReciboNuevo)
        {
            _context.Add(ReciboNuevo);
            _context.SaveChanges();
            return ReciboNuevo;
        }

        public Receipt EliminarRecibo(Guid idRecibo)
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
