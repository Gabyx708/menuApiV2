using Application.Interfaces.IDescuento;
using Domain.Entities;
using Infraestructure.Persistence;

namespace Infraestructure.Commands
{
    public class DescuentoCommand : IDescuentoCommand
    {
        private readonly MenuAppContext _context;

        public DescuentoCommand(MenuAppContext context)
        {
            _context = context;
        }

        public Discount createDescuento(Discount descuento)
        {
            _context.Add(descuento);
            _context.SaveChanges();
            return descuento;

        }
    }
}
