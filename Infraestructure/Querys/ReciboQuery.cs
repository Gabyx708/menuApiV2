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

        public List<Receipt> GetAll()
        {
            return _context.Recibos.ToList();
        }

        public List<Receipt> GetAllByDescuento(Guid idDescuento)
        {
            return _context.Recibos.Where(r => r.IdDescuento == idDescuento).ToList();
        }

        public List<Receipt> GetAllByPersonal(Guid idPersonal)
        {
            throw new NotImplementedException();
        }

        public Receipt GetById(Guid id)
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
