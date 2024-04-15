using Application.Interfaces.IRecibo;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class ReciboQuery : IReciboQuery
    {
        private readonly MenuAppContext _context;

        public ReciboQuery(MenuAppContext context)
        {
            _context = context;
        }

        public List<Recibo> GetAll()
        {
            return _context.Recibos.ToList();
        }

        public List<Recibo> GetAllByDescuento(Guid idDescuento)
        {
            return _context.Recibos.Where(r => r.IdDescuento == idDescuento).ToList();
        }

        public List<Recibo> GetAllByPersonal(Guid idPersonal)
        {
            throw new NotImplementedException();
        }

        public Recibo GetById(Guid id)
        {
            var found = _context.Recibos.FirstOrDefault(r => r.IdRecibo == id);

            if (found == null)
            {
                return null;
            }

            return found;
        }
    }
}
