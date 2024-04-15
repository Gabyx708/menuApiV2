using Application.Interfaces.IPedidoPorMenuPlatillo;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class PedidoPorMenuPlatilloCommand : IPedidoPorMenuPlatilloCommand
    {
        private readonly MenuAppContext _context;

        public PedidoPorMenuPlatilloCommand(MenuAppContext context)
        {
            _context = context;
        }

        public PedidoPorMenuPlatillo CreatePedidoPorMenuPlatillo(PedidoPorMenuPlatillo pedidoPorMenuPlatillo)
        {
            _context.Add(pedidoPorMenuPlatillo);
            _context.SaveChanges();
            return pedidoPorMenuPlatillo;
        }

        public PedidoPorMenuPlatillo DeletePedidoPorMenuPlatillo(Guid idPedidoPorMenuPlatillo)
        {
            var found = _context.PedidosPorMenuPlatillo.FirstOrDefault(pm => pm.IdMenuPlatillo == idPedidoPorMenuPlatillo);

            if (found != null)
            {
                _context.PedidosPorMenuPlatillo.Remove(found);
                _context.SaveChanges();
                return found;
            }

            return null;
        }
    }
}
