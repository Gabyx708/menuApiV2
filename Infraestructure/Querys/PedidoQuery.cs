using Application.Interfaces.IPedido;
using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Querys
{
    public class PedidoQuery : IPedidoQuery
    {
        private readonly MenuAppContext _context;

        public PedidoQuery(MenuAppContext context)
        {
            _context = context;
        }

        public List<Pedido> GetAll()
        {
            return _context.Pedidos.ToList();
        }

        public Pedido GetPedidoById(Guid idPedido)
        {
            var found = _context.Pedidos.FirstOrDefault(p => p.IdPedido == idPedido);

            if (found != null)
            {
                found.PedidosPorMenuPlatillo = _context.PedidosPorMenuPlatillo
                        .Where(pmp => pmp.IdPedido == found.IdPedido).ToList();

                return found;
            }

            return null;
        }

        public List<Pedido> GetPedidosMenu(Guid idMenu)
        {
            List<Guid> menuPlatillo = _context.MenuPlatillos.Where(mp => mp.IdMenu == idMenu).
                                       Select(mp => mp.IdMenuPlatillo).ToList();

            List<Guid> idPedidosDelMenu = new List<Guid>();
            List<Pedido> pedidosDelMenuEncontrados = new List<Pedido>();

            for (int i = 0; i < menuPlatillo.Count; i++)
            {
                List<Guid> encontrados = _context.PedidosPorMenuPlatillo.Where(pmp => pmp.IdMenuPlatillo == menuPlatillo[i]).
                      Select(pmp => pmp.IdPedido).ToList();


                foreach (var id in encontrados)
                {
                    idPedidosDelMenu.Add(id);
                }
            }

            foreach (var idPedido in idPedidosDelMenu)
            {
                pedidosDelMenuEncontrados.Add(GetPedidoById(idPedido));
            }

            return pedidosDelMenuEncontrados;
        }

        public List<Pedido> GetPedidosFiltrado(Guid? idPersonal, DateTime? fechaDesde, DateTime? fechaHasta, int? ultimos)
        {
            IQueryable<Pedido> query = _context.Pedidos.Include(p => p.Personal).Include(p => p.Recibo).AsQueryable();

            if (idPersonal.HasValue)
            {
                query = query.Where(p => p.IdPersonal == idPersonal);
            }

            if (fechaDesde.HasValue)
            {
                if (!fechaHasta.HasValue || fechaDesde.Value.Date == fechaHasta.Value.Date)
                {
                    query = query.Where(p => p.FechaDePedido.Date == fechaDesde.Value.Date);
                }
                else
                {
                    query = query.Where(p => p.FechaDePedido >= fechaDesde);
                }
            }

            if (fechaHasta.HasValue && (!fechaDesde.HasValue || fechaDesde.Value.Date != fechaHasta.Value.Date))
            {
                query = query.Where(p => p.FechaDePedido <= fechaHasta);
            }

            if (ultimos.HasValue)
            {
                query = query.OrderByDescending(p => p.FechaDePedido).Take(ultimos.Value);
            }

            List<Pedido> pedidosFiltrados = query.ToList();

            return pedidosFiltrados;
        }

    }
}
