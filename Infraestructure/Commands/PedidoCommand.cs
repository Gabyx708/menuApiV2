using Application.Interfaces.IPedido;
using Application.Interfaces.IRecibo;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class PedidoCommand : IPedidoCommand
    {
        private readonly MenuAppContext _context;
        private readonly IReciboCommand _reciboCommand;

        public PedidoCommand(MenuAppContext context, IReciboCommand reciboCommand)
        {
            _context = context;
            _reciboCommand = reciboCommand;
        }

        public Pedido createPedido(Pedido pedido)
        {
            _context.Add(pedido);
            _context.SaveChanges();
            return pedido;
        }

        public Pedido DeletePedido(Guid idPedido)
        {
            var found = _context.Pedidos.FirstOrDefault(p => p.IdPedido == idPedido);

            if (found != null)
            {
                var pedidosPorMenuPlatillo = _context.PedidosPorMenuPlatillo.Where(pmp => pmp.IdPedido == found.IdPedido).ToList();

                foreach (var MenuPlatillo in pedidosPorMenuPlatillo)
                {
                    var menuPlatilloModificar = _context.MenuPlatillos.FirstOrDefault(mp => mp.IdMenuPlatillo == MenuPlatillo.IdMenuPlatillo);
                    menuPlatilloModificar.Solicitados = menuPlatilloModificar.Solicitados - 1;
                }

                _context.Remove(found);
                _context.SaveChanges();
                _reciboCommand.EliminarRecibo(found.IdRecibo);
            }

            return null;
        }
    }
}
