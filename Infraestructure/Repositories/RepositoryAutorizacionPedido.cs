using Application.Interfaces.IAutorizacionPedido;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Repositories
{
    public class RepositoryAutorizacionPedido : IRepositoryAutorizacionPedido
    {
        private readonly MenuAppContext _context;

        //TODO arreglar problemas de multiples instancias
        public RepositoryAutorizacionPedido(MenuAppContext context)
        {
            _context = context;
        }

        public AutorizacionPedido CreateAutorizacionPedido(AutorizacionPedido entity)
        {
            _context.AutorizacionPedidos.Add(entity);
            _context.SaveChanges(); // Guardar los cambios de manera asincrónica en la base de datos

            return entity; // Devolver la entidad creada
        }

        public AutorizacionPedido DeleteAutorizacionPedido(Guid idPedido, Guid idPersonal)
        {
            var found = _context.AutorizacionPedidos.FirstOrDefault(ap => ap.IdPedido == idPedido && ap.IdPersonal == idPersonal);

            if (found != null)
            {
                _context.AutorizacionPedidos.Remove(found);
                _context.SaveChanges();
                return found;
            }

            throw new Exception();
        }

        public AutorizacionPedido GetAutorizacionPedidoByidPedido(Guid idPedido)
        {
            var found = _context.AutorizacionPedidos.FirstOrDefault(ap => ap.IdPedido == idPedido);

            if (found != null)
            {
                return found;
            }

            return null;
        }

        public List<AutorizacionPedido> GetAutorizacionesPedidoByIdPersonal(Guid idPersonal)
        {
            var autorizaciones = _context.AutorizacionPedidos.Where(ap => ap.IdPersonal == idPersonal).ToList();
            return autorizaciones;
        }

    }
}
