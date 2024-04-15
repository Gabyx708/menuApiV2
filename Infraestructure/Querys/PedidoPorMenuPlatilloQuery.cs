using Application.Interfaces.IPedidoPorMenuPlatillo;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Querys
{
    public class PedidoPorMenuPlatilloQuery : IPedidoPorMenuPlatilloQuery
    {
        private readonly MenuAppContext _context;

        public PedidoPorMenuPlatilloQuery(MenuAppContext context)
        {
            _context = context;
        }

        public List<PedidoPorMenuPlatillo> GetPedidoMenuPlatilloByMenu(Guid idMenu)
        {
            List<MenuPlatillo> menuPlatillosDeMenu = _context.MenuPlatillos.Where(mp => mp.IdMenu == idMenu).ToList();
            List<PedidoPorMenuPlatillo> platillosDelPedido = new List<PedidoPorMenuPlatillo>();

            for (int i = 0; i < menuPlatillosDeMenu.Count; i++)
            {
                var menuPlatilloDelPedido = _context.PedidosPorMenuPlatillo.Where(ppm => ppm.IdMenuPlatillo == menuPlatillosDeMenu[i].IdMenuPlatillo).ToList();

                foreach (var platillo in menuPlatilloDelPedido)
                {
                    platillosDelPedido.Add(platillo);
                }

            }

            return platillosDelPedido;
        }

        public List<PedidoPorMenuPlatillo> GetPedidoMenuPlatilloByPedido(Guid idPedido)
        {
            return _context.PedidosPorMenuPlatillo.Where(pm => pm.IdPedido == idPedido).ToList();
        }

        public PedidoPorMenuPlatillo GetPedidoPorMenuPlatillo(Guid idPedido, Guid idMenuPlatillo)
        {
            var found = _context.PedidosPorMenuPlatillo.FirstOrDefault(pm => pm.IdPedido == idPedido && pm.IdMenuPlatillo == idMenuPlatillo);

            if (found != null)
            {
                return found;
            }

            return null;
        }
    }
}
